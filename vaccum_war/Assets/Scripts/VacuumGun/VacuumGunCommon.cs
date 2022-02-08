using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumWar
{
    public enum HeadMode
    {
        Empty = 0,
        Roller,
        Shooter,
    }

    public static class VacuumGunCommon
    {
        private static Dictionary<HeadMode, Type> _headModeTypePairs = new Dictionary<HeadMode, Type>() {
            {HeadMode.Roller, typeof(VacuumGunAccessoryRollerHead) },
            {HeadMode.Shooter, typeof(VacuumGunAccessoryShooterHead) },
        };

        private static Dictionary<Type, HeadMode> _headTypeModePairs = new Dictionary<Type, HeadMode>();

        static VacuumGunCommon()
        {
            foreach(var p in _headModeTypePairs)
            {
                _headTypeModePairs.Add(p.Value, p.Key);
            }
        }

        public static Type GetHeadTypeFromMode(HeadMode mode)
        {
            Type type;
            _headModeTypePairs.TryGetValue(mode, out type);
            return type;
        }

        public static HeadMode GetHeadModeFromType(Type type)
        {
            HeadMode mode;
            _headTypeModePairs.TryGetValue(type, out mode);
            return mode;
        }
    }
}