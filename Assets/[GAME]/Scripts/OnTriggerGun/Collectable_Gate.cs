using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// collectable math gate
/// </summary>

namespace GAME
{
    public class Collectable_Gate : OnTriggerGun
    {
        #region Properties
        [SerializeField] Collider otherGate;
        [SerializeField] int addedStickman;
        TextMeshPro tmpro; 
        #endregion

        #region Awake, Init
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            tmpro = GetComponentInChildren<TextMeshPro>();
            tmpro.text = (addedStickman > 0) ? "+" + addedStickman.ToString() : tmpro.text;
            tmpro.text = (addedStickman < 0) ? addedStickman.ToString() : tmpro.text;
        }
        #endregion

        #region Override
        /// <summary>
        /// gate selected, remove the gate chosen
        /// and deactivate access to other gate, we can only pick one
        /// 
        /// add or remove stickmans
        /// 
        /// </summary>
        public override void OnTrigger()
        {
            otherGate.enabled = false;

            gameObject.SetActive(false);

            if (addedStickman > 0)
                EventManager.StickmanUpdateEvent(addedStickman, null, VisualMode.GateCollect);

            if (addedStickman < 0)
                EventManager.StickmanUpdateEvent(addedStickman, null, VisualMode.Remove);
        } 
        #endregion
    }
}
