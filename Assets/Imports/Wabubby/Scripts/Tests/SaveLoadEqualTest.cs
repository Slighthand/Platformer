using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static Wabubby.Extensions;

public class SaveLoadEqualTest {

    private string TestPath { 
        get {
            if (! Directory.Exists($"{Application.persistentDataPath}\\Tests")) {
                Directory.CreateDirectory($"{Application.persistentDataPath}\\Tests");
            }
            return $"{Application.persistentDataPath}\\Tests";
        }
    }

    [Test]
    public void TestTest() {

        Assert.IsTrue(1==1);
    }

    
    [Test]
    public void CreateTest() {
        string path = $"{TestPath}\\CreateTest";

        if (JsonSaver.FileExists(path)) {
            JsonSaver.DestroyFile(path);
        }
        JsonSaver.CreateFile(path);
        bool IsExist = JsonSaver.FileExists(path);

        JsonSaver.DestroyFile(path);

        Assert.IsTrue(IsExist);
    }

    [Test]
    public void DestroyTest() {
        string path = $"{TestPath}\\DestroyTest";

        if (JsonSaver.FileExists(path)) {
            JsonSaver.DestroyFile(path);
        }
        JsonSaver.CreateFile(path);

        JsonSaver.DestroyFile(path);
        bool IsDestroyed = JsonSaver.FileExists(path);

        Assert.IsTrue(IsDestroyed);
    }


    [UnityTest]
    public IEnumerator RunCreateTest() {
        string path = $"{TestPath}\\RunCreateTest";

        if (JsonSaver.FileExists(path)) {
            JsonSaver.DestroyFile(path);
        }
        JsonSaver.CreateFile(path);

        yield return GetWait(0.1f);

        bool IsExist = JsonSaver.FileExists(path);

        JsonSaver.DestroyFile(path);

        Assert.IsTrue(IsExist);
    }
    

    /*
    [Test]
    public void SaveLoadIsEqualTest() {
        SerializableObject objectToSerialize = new SerializableObject("yo what is up dog", 12);
        string path = $"{Application.persistentDataPath}\\SaveLoadIsEqualTest";
        if (JsonSaver.FileExists(path)) {
            JsonSaver.DestroyFile(path);
        }

        JsonSaver.CreateFile(path);
        JsonSaver.Save(objectToSerialize, path);

        bool isEqual = objectToSerialize.Equals(JsonSaver.Load<SerializableObject>(path));

        JsonSaver.DestroyFile(path);


        Assert.IsTrue(isEqual);
    }
    */
    
    /*
    [Test]
    public void EncryptedSaveLoadIsEqualTest() {
        SerializableObject objectToSerialize = new SerializableObject("yo what is up dog", 12);
        string path = $"{Application.persistentDataPath}\\EncryptedSaveLoadIsEqualTest";
        if (JsonSaver.FileExists(path)) {
            JsonSaver.DestroyFile(path);
        }

        JsonSaver.CreateFile(path);
        JsonSaver.SaveEncrypted(objectToSerialize, path);

        bool isEqual = objectToSerialize.Equals(JsonSaver.LoadEncrypted(path));

        JsonSaver.DestroyFile(path);


        Assert.IsTrue(isEqual);
    }
    */

    [Serializable]
    public class SerializableObject {
        public string a;
        public int b;

        public SerializableObject(string a, int b) {
            this.a = a;
            this.b = b;
        }

        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            
            return (a == ((SerializableObject) obj).a) && (b == ((SerializableObject) obj).b);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new System.NotImplementedException();
            return base.GetHashCode();
        }
    }

}
