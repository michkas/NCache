//  Copyright (c) 2019 Alachisoft
//  
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//     http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Alachisoft.NCache.Serialization.Formatters
{
	/// <summary>
	/// class to assist with common system and .NET operations.
	/// </summary>
	public class NativeBinaryFormatter
	{
		/// <summary>
		/// serialize an objevt into a stream.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="objectToSend"></param>
		public static bool Serialize(Stream stream, object objectToSend)
		{
			try
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, objectToSend);
				return true;
			}
			catch (Exception e)
			{
                //Trace.error("Common.Serialize()", e.ToString());
				throw;
			}
		}

		/// <summary>
		/// deserialize an object from the stream.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static object Deserialize(Stream stream)
		{
			try
			{
				BinaryFormatter formatter = new BinaryFormatter();
				return formatter.Deserialize(stream);
			}
			catch (Exception e)
			{
                //Trace.error("Common.Deserialize()", e.ToString());
				throw;
			}
		}

		/// <summary>
		/// Creates an object from a byte buffer
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public static object FromByteBuffer(byte[] buffer)
		{
			if (buffer == null) return null;
			using (MemoryStream ms = new MemoryStream(buffer))
			{
				return Deserialize(ms);
			}
		}

		/// <summary>
		/// Serializes an object into a byte buffer.
		/// The object has to implement interface Serializable or Externalizable
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static byte[] ToByteBuffer(object obj)
		{
			if (obj == null) return null;
			using (MemoryStream ms = new MemoryStream())
			{
				Serialize(ms, obj);
				return ms.ToArray();
			}
		}
	}

}
