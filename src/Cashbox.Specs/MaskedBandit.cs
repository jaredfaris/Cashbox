// Copyright 2010 Travis Smith
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Cashbox.Specs
{
	using System;
	using System.Diagnostics;
	using NUnit.Framework;


	[TestFixture]
	public class MaskedBandit
	{
		[Test]
		public void Insert_10k_records()
		{
			var insertStoreName = "10k_insert.store";
			using (IDocumentSession session = DocumentSessionFactory.Create(insertStoreName))
			{
				Stopwatch sw = new Stopwatch();
				sw.Start();
				for (int i = 0; i < 10000; i++)
				{
					session.Store(i.ToString(), new NumericDocument
						{
							Number = i
						});
				}
				sw.Stop();

				Console.WriteLine("10k inserts: {0}ms", sw.ElapsedMilliseconds);
			}

			using(var session = DocumentSessionFactory.Create(insertStoreName))
			{
				for (int i = 0; i < 10000; i++)
				{
					var result = session.Retrieve<NumericDocument>(i.ToString()).Number;

					Assert.That(result, Is.EqualTo(i));
				}
			}
		}
	}
}