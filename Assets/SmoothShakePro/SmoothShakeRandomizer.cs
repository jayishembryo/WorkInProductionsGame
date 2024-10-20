using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Randomizer")]
    public class SmoothShakeRandomizer : MonoBehaviour
    {
        public float randomAmplitude = 0f, randomFrequency = 0.5f, randomOffset = 0, randomPhase = 0.5f;
        public bool randomizeAxisEvenly = false;

        public List<ShakeBase> shakes;

        private float GetRandom(float addRandom)
        {
            return Random.Range(-addRandom, addRandom);
        }

        /// <summary>
        /// Randomize the shakes based on the random values set in the inspector
        /// </summary>
        public void Randomize()
        {
            Randomize(randomAmplitude, randomFrequency, randomOffset, randomPhase);
        }

        /// <summary>
        /// Randomize the shakes based on the given random values
        /// </summary>
        /// <param name="randomAmplitude"></param>
        /// <param name="randomFrequency"></param>
        /// <param name="randomOffset"></param>
        /// <param name="randomPhase"></param>
        public void Randomize(float randomAmplitude, float randomFrequency, float randomOffset, float randomPhase)
        {
            for (int i = 0; i < shakes.Count; ++i)
            {
                ShakeBase shake = shakes[i];
                if (shake == null) continue;

                switch (shake)
                {
                    case MultiShakeBase msb:
                        foreach (IEnumerable<Shaker> shakers in msb.GetMultiShakers())
                        {
                            foreach (Shaker shaker in shakers)
                            {
                                if (shaker is MultiFloatShaker mfs)
                                {
                                    mfs.amplitude += GetRandom(randomAmplitude);
                                    mfs.frequency += GetRandom(randomFrequency);
                                    mfs.offset += GetRandom(randomOffset);
                                    mfs.phase += GetRandom(randomPhase);
                                }
                                if (shaker is MultiVectorShaker mvs)
                                {
                                    if (!randomizeAxisEvenly)
                                    {
                                        mvs.amplitude += new Vector3(GetRandom(randomAmplitude), GetRandom(randomAmplitude), GetRandom(randomAmplitude));
                                        mvs.frequency += new Vector3(GetRandom(randomFrequency), GetRandom(randomFrequency), GetRandom(randomFrequency));
                                        mvs.offset += new Vector3(GetRandom(randomOffset), GetRandom(randomOffset), GetRandom(randomOffset));
                                        mvs.phase += new Vector3(GetRandom(randomPhase), GetRandom(randomPhase), GetRandom(randomPhase));
                                    }
                                    else
                                    {
                                        float random = GetRandom(randomAmplitude);
                                        mvs.amplitude += new Vector3(random, random, random);
                                        random = GetRandom(randomFrequency);
                                        mvs.frequency += new Vector3(random, random, random);
                                        random = GetRandom(randomOffset);
                                        mvs.offset += new Vector3(random, random, random);
                                        random = GetRandom(randomPhase);
                                        mvs.phase += new Vector3(random, random, random);
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        Debug.LogWarning("Shake type of " + shake.GetType() + " is not (yet) supported by SmoothShakeRandomizer");
                        break;
                }
            }
        }
    }

}
