using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TopDown
{
    [CreateAssetMenu(menuName = "Scriptable/Player types", fileName = "PlayerTypes")]
    public class PlayerTypes : ScriptableObject
    {
        [field: SerializeField] public List<PlayerTypeItem> PlayerTypeItems { get; private set; }

        public PlayerTypeItem GetPlayerTypeItem(PlayerType type) =>
            PlayerTypeItems.First(playerType => playerType.PlayerType == type);
    }

    [Serializable]
    public class PlayerTypeItem
    {
        [field: SerializeField] public PlayerType PlayerType { get; private set; }
        [field: SerializeField] public PlayerInfo PlayerInfo { get; private set; }
    }
}