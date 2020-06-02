﻿using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barebones.Bridges.Mirror.Character
{
    public delegate void VitalChangeFloatDelegate(short key, float value);

    public class PlayerCharacterVitals : PlayerCharacterBehaviour
    {
        /// <summary>
        /// Called when player resurrected
        /// </summary>
        public event Action OnAliveEvent;

        /// <summary>
        /// Called when player dies
        /// </summary>
        public event Action OnDieEvent;

        /// <summary>
        /// Called on client when one of the vital value is changed
        /// </summary>
        public event VitalChangeFloatDelegate OnVitalChangedEvent;

        /// <summary>
        /// Check if character is alive
        /// </summary>
        public bool IsAlive { get; protected set; } = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void NotifyVitalChanged(short key, float value)
        {
            if (isServer)
            {
                Rpc_NotifyVitalChanged(key, value);
            }
        }

        [ClientRpc]
        private void Rpc_NotifyVitalChanged(short key, float value)
        {
            if (isLocalPlayer)
            {
                OnVitalChangedEvent?.Invoke(key, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void NotifyAlive()
        {
            if (isServer)
            {
                IsAlive = true;
                Rpc_NotifyAlive();
            }
        }

        [ClientRpc]
        private void Rpc_NotifyAlive()
        {
            if (isLocalPlayer)
            {
                IsAlive = true;
                OnAliveEvent?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void NotifyDied()
        {
            if (isServer)
            {
                IsAlive = false;
                Rpc_NotifyDied();
            }
        }

        [ClientRpc]
        private void Rpc_NotifyDied()
        {
            if (isLocalPlayer)
            {
                IsAlive = false;
                OnDieEvent?.Invoke();
            }
        }
    }
}