using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CsvDemo.Analysers
{

    /// <summary>
    /// Streaming <see cref="Person"/> analyser
    /// </summary>
    /// <remarks>
    /// Analyses a stream of <see cref="Person"/>s, maintaining running track of various pieces of aggregate information
    /// which can be retrieved at any time.
    /// </remarks>
    public class PeopleAnalyser
    {

        /// <summary>
        /// Initialise a new people analyser
        /// </summary>
        public PeopleAnalyser()
        {
            nameTallies = new Dictionary<string, int>();
            uniqueAddresses = new HashSet<string>();
        }


        /// <summary>
        /// A running tally of all names encountered (both first and last) along with the number of times each has been
        /// encountered
        /// </summary>
        public IReadOnlyDictionary<string, int> NameTallies
        {
            get
            {
                return new ReadOnlyDictionary<string, int>(nameTallies);
            }
        }


        /// <summary>
        /// A running list of all unique addresses encountered
        /// </summary>
        public IReadOnlyCollection<string> UniqueAddresses
        {
            get
            {
                return new ReadOnlyCollection<string>(new List<string>(uniqueAddresses));
            }
        }


        /// <summary>
        /// Process the next <see cref="Person"/>
        /// </summary>
        /// <param name="person">
        /// The <see cref="Person"/> to process
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="person"/> was <c>null</c>
        /// </exception>
        public void Process(Person person)
        {
            if (person == null) throw new ArgumentNullException("person");

            // Maintain name tally
            if (!NameTallies.ContainsKey(person.FirstName)) nameTallies.Add(person.FirstName, 0);
            nameTallies[person.FirstName]++;
            if (!NameTallies.ContainsKey(person.LastName)) nameTallies.Add(person.LastName, 0);
            nameTallies[person.LastName]++;

            // Maintain unique address list
            uniqueAddresses.Add(person.Address);
        }


        IDictionary<string, int> nameTallies;


        ISet<string> uniqueAddresses;

    }
}
