﻿using System;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Serialization
{
    public interface IObjectSerializer
    {
        T Deserialize<T>(string xmlString) where T : class;
        void Serialize(Type serializableType, object type);
    }
}