using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;

namespace Firebase_Firestore {
    class Program {

        private static FirestoreDb firestoreDb;
        private static readonly string GOOGLE_CREDENTIALS = "GOOGLE_APPLICATION_CREDENTIALS";
        private static readonly string COLLECTION_CITIES = "City";

        static void Main(string[] _) {

            string path_to_json = AppDomain.CurrentDomain.BaseDirectory + @"itcemail.json"; // PUT YOUR OWN PATH
            GET_GOOGLE_CREDENTIALS(path: path_to_json);

            firestoreDb = FirestoreDb.Create("itcemail");

            Dictionary<string, object> LosAngeles = new Dictionary<string, object>(){
                { "Name", "Los Angeles" },
                { "State", "CA" },
                { "Country", "USA" },
                { "Capital", false },
                { "Population", 3900000 }
            };

            City Tokyo = new City {
                Name = "Tokyo" ,
                State = null,
                Country=  "Japan",
                Capital= true,
                Population= 9000000
            };

            AddWithDictionary(GetCollection(COLLECTION_CITIES), document: "LA", data: LosAngeles);
            AddWithClass(GetCollection(COLLECTION_CITIES), document: "TOK", data: Tokyo);

            GetCity();

            Console.ReadKey();
        }


        private static void GET_GOOGLE_CREDENTIALS(string path) {
            Environment.SetEnvironmentVariable(GOOGLE_CREDENTIALS, path);
        }


        private async static void AddWithDictionary(CollectionReference collectionRef, string document, Dictionary<string, object> data) {
            await collectionRef.Document(document).SetAsync(data);
        }

        private async static void AddWithClass<T>(CollectionReference collectionRef, string document, T data) {
            await collectionRef.Document(document).SetAsync(data);
        }

        private async static void UpdateWithDictionary(CollectionReference collectionRef, string document, Dictionary<string, object> data) {
            await collectionRef.Document(document).UpdateAsync(data);
        }

        private static CollectionReference GetCollection(string collectionName) {
            return firestoreDb.Collection(collectionName);
        }

        private static async void GetCity() {

            Query capitalQuery = firestoreDb.Collection(COLLECTION_CITIES); // GETTING DATA FROM City Collection
            QuerySnapshot capitalQuerySnapshot = await capitalQuery.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in capitalQuerySnapshot.Documents) {

                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
                Dictionary<string, object> city = documentSnapshot.ToDictionary();

                foreach (KeyValuePair<string, object> pair in city) {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }

                Console.WriteLine("");
            }


        }

    }
}
