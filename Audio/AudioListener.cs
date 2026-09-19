using FMODUnity;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Audio listener wrapper.
    /// This wrapper is used to allow the movement of the unique audio listener. 
    /// </summary>
    [RequireComponent(typeof(StudioListener))]
    public class AudioListener : MonoBehaviour
    {
        /// <summary>
        /// The local static studio reference to move.
        /// </summary>
        private static StudioListener _studioListener;

        /// <summary>
        /// Make sure to enable the studio listener one time.
        /// </summary>
        private bool _listenerAlreadyActivated;

        /// <summary>
        /// Auto set the studio listener.
        /// </summary>
        private void Awake() => _studioListener = GetComponent<StudioListener>();

        /// <summary>
        /// Enable the studio listener after the first update cycle.
        /// </summary>
        private void LateUpdate()
        {
            if (_listenerAlreadyActivated) return;
            
            if (_studioListener)
            {
                _listenerAlreadyActivated = true;
                _studioListener.enabled = true;
            }
        }

        /// <summary>
        /// Move the audio listener in world.
        /// </summary>>
        public static void SetListenerPosition(Vector2 position)
        {
            if(_studioListener == null) return;
            _studioListener.transform.position = position;
        }
    }
}