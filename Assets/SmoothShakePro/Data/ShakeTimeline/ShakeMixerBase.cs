using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SmoothShakePro
{
    public abstract class ShakeMixerBase : PlayableBehaviour
    {
        private Vector3[] sum;

        public ShakeBase trackBinding;

        protected abstract int GetMultiShakersLength();

        public override void OnGraphStart(Playable playable)
        {
            sum = new Vector3[GetMultiShakersLength()];
            base.OnGraphStart(playable);
        }

        public override void OnGraphStop(Playable playable)
        {
            if (trackBinding != null)
                trackBinding.ResetDefaultValues();
            base.OnGraphStop(playable);
        }

        public T[] GetBehaviours<T>(Playable playable) where T : ShakeBehaviourBase, new()
        {
            int inputCount = playable.GetInputCount(); // Get the number of all clips on this track
            T[] behaviours = new T[inputCount]; // Create an array to store the ShakeBehaviourBase of each clip

            for (int i = 0; i < inputCount; i++)
            {
                behaviours[i] = ((ScriptPlayable<T>)playable.GetInput(i)).GetBehaviour(); // Get the ShakeBehaviourBase of each clip
            }

            return behaviours;
        }


        public void ProcessShake(ShakeBase trackBinding, Playable playable, ShakeBehaviourBase[] behaviour)
        {
            if (this.trackBinding == null)
            {
                this.trackBinding = trackBinding;
                this.trackBinding.SaveDefaultValues();
            }

            int inputCount = playable.GetInputCount();

            //Reset sum values
            if (sum == null)
            {
                Debug.LogWarning("Sum is null");
                return;
            }

            for (int i = 0; i < sum.Length; i++) sum[i] = Vector3.zero;

            for (int i = 0; i < inputCount; i++)
            {
                if (behaviour[i] == null)
                {
                    Debug.LogWarning($"Behaviour at index {i} is null");
                    continue;
                }

                IEnumerable<Shaker>[] currentShakers = behaviour[i].shakers;
                float originalInputWeight = playable.GetInputWeight(i);
                if(originalInputWeight > 0)
                {
                    for (int y = 0; y < currentShakers.Length; y++)
                    {
                        if (currentShakers[y] is List<MultiVectorShaker> vectorShakerList)
                        {
                            for (int u = 0; u < vectorShakerList.Count; u++)
                            {
                                Shaker shaker = vectorShakerList[u];

                                float lifetime;
                                if (shaker is MultiVectorShaker multiVectorShaker)
                                    lifetime = multiVectorShaker.lifetime;
                                else
                                    throw new SystemException("Shaker is not a MultiVectorShaker");

                                // Execute the shaker
                                Utility.ExecuteMultiShaker((float)playable.GetTime(), shaker, CalculateInputWeight(i, lifetime, playable, behaviour), sum, y, 1, GetPhase(shaker));
                            }
                        }
                        else if (currentShakers[y] is List<MultiFloatShaker> floatShakerList)
                        {
                            for (int u = 0; u < floatShakerList.Count; u++)
                            {
                                Shaker shaker = floatShakerList[u];

                                float lifetime;
                                if (shaker is MultiFloatShaker multiFloatShaker)
                                    lifetime = multiFloatShaker.lifetime;
                                else
                                    throw new SystemException("Shaker is not a MultiFloatShaker");

                                // Execute the shaker
                                Utility.ExecuteMultiShaker((float)playable.GetTime(), shaker, CalculateInputWeight(i, lifetime, playable, behaviour), sum, y, 1, GetPhase(shaker));
                            }
                        }
                    }
                }
            }
            trackBinding.Apply(sum);
        }

        private Vector3 GetPhase(Shaker shaker)
        {
            if (shaker is MultiVectorShaker multiVectorShaker)
                return multiVectorShaker.phase;
            else if (shaker is MultiFloatShaker multiFloatShaker)
                return new Vector3(multiFloatShaker.phase, 0, 0);

            return Vector3.zero;
        }

        private float CalculateInputWeight(int i, float lifetime, Playable playable, ShakeBehaviourBase[] behaviour)
        {
            return playable.GetInputWeight(i);
        }
    }

}
