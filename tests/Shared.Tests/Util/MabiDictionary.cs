// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using Aura.Shared.Mabi;
using Aura.Shared.Network;
using Xunit;

namespace Aura.Tests.Shared.Mabi
{
	public class MabiDictionaryTests
	{
		/// <summary>
		/// Settings and getting values.
		/// </summary>
		[Fact]
		public void SettingValues()
		{
			var dict = new MabiDictionary();
			dict.SetBool("boolTest", true);
			dict.SetByte("byteTest", 1);
			dict.SetShort("shortTest", 2);
			dict.SetInt("intTest", 4);
			dict.SetFloat("floatTest", 4.0f);
			dict.SetLong("longTest", 8);
			dict.SetString("stringTest", "1234");

			Assert.Equal(true, dict.GetBool("boolTest"));
			Assert.Equal(1, dict.GetByte("byteTest"));
			Assert.Equal(2, dict.GetShort("shortTest"));
			Assert.Equal(4, dict.GetInt("intTest"));
			Assert.Equal(4.0f, dict.GetFloat("floatTest"));
			Assert.Equal(8, dict.GetLong("longTest"));
			Assert.Equal("1234", dict.GetString("stringTest"));
		}

		/// <summary>
		/// Serializing dictionary to string.
		/// </summary>
		[Fact]
		public void Serializing()
		{
			var dict = new MabiDictionary();
			dict.SetBool("boolTest", true);
			dict.SetByte("byteTest", 1);
			dict.SetShort("shortTest", 2);
			dict.SetInt("intTest", 4);
			dict.SetFloat("floatTest", 4.0f);
			dict.SetLong("longTest", 8);
			dict.SetString("stringTest", "1234;:5678");

			Assert.Equal("boolTest:b:1;byteTest:1:1;shortTest:2:2;intTest:4:4;floatTest:f:4;longTest:8:8;stringTest:s:1234%S%C5678;", dict.ToString());
		}

		/// <summary>
		/// Deserializing dictionary from string.
		/// </summary>
		[Fact]
		public void Deserializing()
		{
			var dict = new MabiDictionary("boolTest:b:1;byteTest:1:1;shortTest:2:2;intTest:4:4;floatTest:f:4;longTest:8:8;stringTest:s:1234%S%C5678;");

			Assert.Equal(true, dict.GetBool("boolTest"));
			Assert.Equal(1, dict.GetByte("byteTest"));
			Assert.Equal(2, dict.GetShort("shortTest"));
			Assert.Equal(4, dict.GetInt("intTest"));
			Assert.Equal(4.0f, dict.GetFloat("floatTest"));
			Assert.Equal(8, dict.GetLong("longTest"));
			Assert.Equal("1234;:5678", dict.GetString("stringTest"));
		}
	}
}
