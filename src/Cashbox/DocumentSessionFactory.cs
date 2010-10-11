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
namespace Cashbox
{
    using Engines;
    using Implementations;


	public static class DocumentSessionFactory
	{
	    public delegate Engine EngineFactory(string filename);
        
        static EngineFactory _engineFactory;

        static DocumentSessionFactory()
        {
            _engineFactory = str => new SqliteEngine(str);
        }

        public static void SetEngineFactory(EngineFactory engineFactory)
	    {
	        _engineFactory = engineFactory;
	    }

	    public static DocumentSession Create(string filename)
		{
			return new CashboxDocumentSession(_engineFactory(filename));
		}
	}
}