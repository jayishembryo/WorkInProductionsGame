using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuSlide : MonoBehaviour
{
    public RectTransform buttonRectTransform;  // Assign the button's RectTransform
    public float slideDuration = 1.0f;         // Duration of the slide animation
    public Vector2 onScreenPosition;           // Target position on-screen
    public Vector2 offScreenPosition;          // Start position off-screen

    private void Start()
    {
        // Set the button to start off-screen
        buttonRectTransform.anchoredPosition = offScreenPosition;

        // Start sliding in the button with easing
        StartCoroutine(SlideInWithEaseOut());
    }

    private IEnumerator SlideInWithEaseOut()
    {
        float elapsedTime = 0;

        while (elapsedTime < slideDuration)
        {
            // Calculate a normalized time value between 0 and 1
            float t = elapsedTime / slideDuration;

            // Apply ease-out (quadratic) easing function to time
            t = EaseOutQuad(t);

            // Lerp the position from off-screen to on-screen using eased time value
            buttonRectTransform.anchoredPosition = Vector2.Lerp(offScreenPosition, onScreenPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;  // Wait until the next frame
        }

        // Ensure the button is exactly at the on-screen position at the end of the animation
        buttonRectTransform.anchoredPosition = onScreenPosition;
    }

    // Ease-out quadratic function. Slows down as the buttons reach close to ther destination.
    private float EaseOutQuad(float t)
    {
        return t * (2 - t);
    }
}