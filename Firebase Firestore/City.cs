using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase_Firestore {

    [FirestoreData]
    class City {

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string State { get; set; }

        [FirestoreProperty]
        public string Country { get; set; }

        [FirestoreProperty]
        public string CapitalPopulation { get; set; }

        [FirestoreProperty]
        public bool Capital { get; internal set; }

        [FirestoreProperty]
        public int Population { get; internal set; }
    }
}
