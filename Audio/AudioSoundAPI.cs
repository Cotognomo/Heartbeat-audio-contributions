using FMODUnity;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Sound effects implementation.
    /// </summary>
    public static class AudioSoundAPI
    {
        #region Game State Events

        /// <summary>
        /// This is called everytime a game state start.
        /// </summary>
        public static void OnGameStateStart(string stateName)
        {
            switch (stateName)
            {
                case "GameStarted": break;
                case "GameMainMenu": break;
                case "GameOptions": break;
                case "GameExit":
                    RuntimeManager.PlayOneShot("event:/UI/MenuClose");
                    break;
                case "GamePlayerSelection": break;
                case "GameRunning":
                    RuntimeManager.PlayOneShot("event:/Gameplay/GameplayIntroduction");
                    break;

                case "GameCompleted": break;
                case "GameScoreboard":
                    RuntimeManager.PlayOneShot("event:/Gameplay/ScoreScreenEnter");
                    break;
            }
        }
        
        /// <summary>
        /// This is called everytime a game state end.
        /// </summary>
        public static void OnGameStateEnd(string stateName)
        {
            switch (stateName)
            {
                case "GameStarted": break;
                case "GameMainMenu": break;
                case "GameOptions": break;
                case "GameExit": break;
                case "GamePlayerSelection": break;
                case "GameRunning": break;
                case "GameCompleted": break;
                case "GameScoreboard": break;
            }
        }

        #endregion

        #region UI Events
        
        /// <summary>
        /// UI Atmos Effect.
        /// </summary>
        public static void PlayAtmos()
        {
            RuntimeManager.PlayOneShot("event:/UI/Atmos");
        }
        
        /// <summary>
        /// UI Selection.
        /// </summary>
        public static void UISelection(string name)
        {
            RuntimeManager.PlayOneShot("event:/UI/Selection");
        }

        /// <summary>
        /// When return from a game screen.
        /// </summary>
        /// <param name="name">GameState Name</param>
        public static void UIReturnBack(string name)
        {
            RuntimeManager.PlayOneShot("event:/UI/MenuClose");
        }

        /// <summary>
        /// UI Toggle.
        /// </summary>
        public static void UIToggleChange(string name, bool state)
        {
            RuntimeManager.PlayOneShot(state ? "event:/UI/ToggleOn" : "event:/UI/ToggleOff");
        }

        /// <summary>
        /// UI Slider Value Change.
        /// </summary>
        public static void UISliderChange(string name, float value)
        {
            RuntimeManager.PlayOneShot("event:/UI/SliderChange");
        }

        /// <summary>
        /// UI Horizontal Selector Change.
        /// </summary>
        public static void UIHorizontalSelectorChange(string name)
        {
            RuntimeManager.PlayOneShot("event:/UI/HorizontalSelectorChange");
        }

        /// <summary>
        /// UI Beat sound
        /// </summary>
        public static void UIBeatSound()
        {
            RuntimeManager.PlayOneShot("event:/UI/PressPlay");
        }

        /// <summary>
        /// UI Button pressed.
        /// </summary>
        public static void UIButtonPressed(string name)
        {
            RuntimeManager.PlayOneShot("event:/UI/ButtonPressed");
        }

        /// <summary>
        /// The Selected Player Change.
        /// </summary>
        public static void UIPlayerSelectionChange(string name)
        {
            RuntimeManager.PlayOneShot("event:/UI/SelectedPlayer");
        }

        /// <summary>
        /// UI Interaction blocked.
        /// </summary>
        public static void UIBlocked()
        {
            RuntimeManager.PlayOneShot("event:/UI/Blocked");
        }

        #endregion

        #region Gameplay Events

        /// <summary>
        /// The sound of the beat.
        /// </summary>
        public static void Beat()
        {
            RuntimeManager.PlayOneShot("event:/Gameplay/Beat");
        }

        /// <summary>
        /// Called on killchain decrease.
        /// This value changes when the light bar reaches the other side of the screen.
        /// </summary>
        public static void KillchainDecrease()
        {
            RuntimeManager.PlayOneShot("event:/Gameplay/KillchainDecrease");
        }

        /// <summary>
        /// Called on player death.
        /// </summary>
        public static void PlayerDeath()
        {
            RuntimeManager.PlayOneShot("event:/Gameplay/PlayerDeath"); 
        }

        /// <summary>
        /// Called on player ability recharge.
        /// </summary>
        public static void PlayerRechargeAbility()
        {
            RuntimeManager.PlayOneShot("event:/Gameplay/PlayerAbilityRecharge");
        }

        /// <summary>
        /// Called on enemy death.
        /// Bpm and killchain are updated with value AFTER the enemy death. 
        /// </summary>
        /// <param name="eventReference">The event reference to play</param>
        /// <param name="position">The enemy position during death</param>
        public static void EnemyDeath(EventReference eventReference, Vector3 position)
        {
            var instance = RuntimeManager.CreateInstance(eventReference);
            instance.set3DAttributes(position.To3DAttributes());
            instance.start();
            instance.release();
        }

        /// <summary>
        /// Called on each enemy spawn.
        /// This event should have a cooldown/delay in FMOD Studio to ensure that it is called one
        /// at time per beat/spawn group.
        /// </summary>
        public static void EnemySpawn(Vector2 position)
        {
            var instance = RuntimeManager.CreateInstance("event:/Gameplay/EnemyAfterSpawn");
            instance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
            instance.start();
            instance.release();
        }

        /// <summary>
        /// Called when a text appears on screen, used mainly in the final screens.
        /// </summary>
        public static void FinalDialogues()
        {
            RuntimeManager.PlayOneShot("event:/Gameplay/FinalDialogue");
        }

        #endregion

        #region Bullet Sounds
        
        /// <summary>
        /// Called when the bullet starts.
        /// </summary>
        public static void BulletStart(EventReference eventReference)
        {
            if(eventReference.IsNull) return;
            RuntimeManager.PlayOneShot(eventReference);
        }
        
        /// <summary>
        /// Called when the bullet hit something and does not do damage.
        /// </summary>
        public static void BulletHit(EventReference eventReference, Vector3 position)
        {
            if(eventReference.IsNull) return;
            RuntimeManager.PlayOneShot(eventReference, position);
        }
        
        /// <summary>
        /// Called when the bullet hit something and do damage.
        /// </summary>
        public static void BulletDamage(EventReference eventReference, Vector3 position)
        {
            if(eventReference.IsNull) return;
            RuntimeManager.PlayOneShot(eventReference, position);
        }
        
        #endregion
    }
}