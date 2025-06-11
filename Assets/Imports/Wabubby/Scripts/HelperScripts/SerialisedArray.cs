using System.Collections;
using System.Collections.Generic;

namespace Wabubby
{
    /// <summary>
    /// recursive structure to support unity's serialisation of staggered arrays.
    /// if you're using this for a scriptable object, consider [PreferBinarySeriaization]
    /// </summary>
    /// <typeparam name="T"></typeparam>

    [System.Serializable]
    public class Array1<T> {
        public Array1(T[] values){ this.values = values; }

        public T[] values;

        public T this [int index] {
            get => values[index];
            set => values[index] = value;
        }

        public static implicit operator T[](Array1<T> sarray) => sarray.values;

        public static explicit operator Array1<T>(T[] array) => new Array1<T>(array);
    }

    [System.Serializable]
    public class Array2<T> {
        public Array2(Array1<T>[] values){ this.values = values; }
        public Array1<T>[] values;

        public Array1<T> this [int index] {
            get => values[index];
            set => values[index] = value;
        }

        public static explicit operator Array2<T>(T[][] array) {
            Array1<T>[] values = new Array1<T>[array.Length];
            for (int i=0; i<array.Length; i++) {
                values[i] = (Array1<T>) array[i];
            }
            return new Array2<T>(values);
        }
    }

    [System.Serializable]
    public class Array3<T> {
        public Array3(Array2<T>[] values){ this.values = values; }
        public Array2<T>[] values;

        public Array2<T> this [int index] {
            get => values[index];
            set => values[index] = value;
        }

        public static explicit operator Array3<T>(T[][][] array) {
            Array2<T>[] values = new Array2<T>[array.Length];
            for (int i=0; i<array.Length; i++) {
                values[i] = (Array2<T>) array[i];
            }
            return new Array3<T>(values);
        }
    }

    [System.Serializable]
    public class Array4<T> {
        public Array4(Array3<T>[] values){ this.values = values; }
        public Array3<T>[] values;

        public Array3<T> this [int index] {
            get => values[index];
            set => values[index] = value;
        }

        public static explicit operator Array4<T>(T[][][][] array) {
            Array3<T>[] values = new Array3<T>[array.Length];
            for (int i=0; i<array.Length; i++) {
                values[i] = (Array3<T>) array[i];
            }
            return new Array4<T>(values);
        }
    }
}
