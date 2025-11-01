namespace MyVector
{
    public class MyVector<T>
    {
        private T[] elementData;
        private int elementCount;
        private int capacityIncrement;

        private const int DEFAULT_CAPACITY = 10;

        public MyVector(int initialCapacity, int capacityIncrement)
        {
            if (initialCapacity < 0)
                throw new ArgumentException("initialCapacity must be >= 0");

            this.elementData = new T[initialCapacity];
            this.elementCount = 0;
            this.capacityIncrement = capacityIncrement;
        }

        public MyVector(int initialCapacity) : this(initialCapacity, 0)
        {
        }

        public MyVector() : this(DEFAULT_CAPACITY, 0)
        {
        }

        public MyVector(T[] a)
        {
            if (a == null) a = new T[0];
            elementData = new T[Math.Max(DEFAULT_CAPACITY, a.Length)];
            Array.Copy(a, 0, elementData, 0, a.Length);
            elementCount = a.Length;
            capacityIncrement = 0;
        }

        private void EnsureCapacityFor(int minCapacity)
        {
            if (elementData.Length >= minCapacity) return;

            int newCapacity;
            if (capacityIncrement > 0)
            {
                newCapacity = elementData.Length + capacityIncrement;
                if (newCapacity < minCapacity) newCapacity = minCapacity;
            }
            else
            {
                newCapacity = Math.Max(elementData.Length * 2, minCapacity);
                if (newCapacity == 0) newCapacity = 1;
            }

            T[] newData = new T[newCapacity];
            Array.Copy(elementData, newData, elementCount);
            elementData = newData;
        }

        public void Add(T e)
        {
            EnsureCapacityFor(elementCount + 1);
            elementData[elementCount++] = e;
        }

        public void AddAll(T[] a)
        {
            if (a == null) return;
            EnsureCapacityFor(elementCount + a.Length);
            Array.Copy(a, 0, elementData, elementCount, a.Length);
            elementCount += a.Length;
        }

        public void Clear()
        {
            for (int i = 0; i < elementCount; i++) elementData[i] = default!;
            elementCount = 0;
        }

        public bool Contains(object o)
        {
            return IndexOf(o) != -1;
        }

        public bool ContainsAll(T[] a)
        {
            if (a == null) return true;
            foreach (T item in a)
            {
                if (!Contains(item)) return false;
            }

            return true;
        }

        public bool IsEmpty()
        {
            return elementCount == 0;
        }

        public bool Remove(object o)
        {
            int idx = IndexOf(o);
            if (idx == -1) return false;
            RemoveAt(idx);
            return true;
        }

        public bool RemoveAll(T[] a)
        {
            if (a == null || a.Length == 0) return false;
            bool changed = false;
            foreach (T item in a)
            {
                while (Remove(item)) changed = true;
            }

            return changed;
        }

        public bool RetainAll(T[] a)
        {
            if (a == null) return false;
            bool changed = false;
            int write = 0;
            for (int i = 0; i < elementCount; i++)
            {
                T cur = elementData[i];
                bool keep = false;
                foreach (T t in a)
                {
                    if (Equals(cur, t))
                    {
                        keep = true;
                        break;
                    }
                }

                if (keep)
                {
                    elementData[write++] = cur;
                }
                else
                {
                    changed = true;
                }
            }

            for (int i = write; i < elementCount; i++) elementData[i] = default!;
            elementCount = write;
            return changed;
        }

        public int Size()
        {
            return elementCount;
        }

        public object[] ToArray()
        {
            object[] result = new object[elementCount];
            for (int i = 0; i < elementCount; i++) result[i] = elementData[i]!;
            return result;
        }

        public T[] ToArray(T[] a)
        {
            if (a == null)
            {
                T[] copy = new T[elementCount];
                Array.Copy(elementData, 0, copy, 0, elementCount);
                return copy;
            }

            if (a.Length >= elementCount)
            {
                Array.Copy(elementData, 0, a, 0, elementCount);
                if (a.Length > elementCount) a[elementCount] = default!;
                return a;
            }
            else
            {
                T[] copy = new T[elementCount];
                Array.Copy(elementData, 0, copy, 0, elementCount);
                return copy;
            }
        }

        public void Add(int index, T e)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException(nameof(index));
            EnsureCapacityFor(elementCount + 1);
            for (int i = elementCount; i > index; i--) elementData[i] = elementData[i - 1];
            elementData[index] = e;
            elementCount++;
        }

        public void AddAll(int index, T[] a)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException(nameof(index));
            if (a == null || a.Length == 0) return;
            EnsureCapacityFor(elementCount + a.Length);
            for (int i = elementCount - 1; i >= index; i--)
            {
                elementData[i + a.Length] = elementData[i];
            }

            for (int i = 0; i < a.Length; i++) elementData[index + i] = a[i];
            elementCount += a.Length;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException(nameof(index));
            return elementData[index];
        }

        public int IndexOf(object o)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (Equals(elementData[i], o)) return i;
            }

            return -1;
        }

        public int LastIndexOf(object o)
        {
            for (int i = elementCount - 1; i >= 0; i--)
            {
                if (Equals(elementData[i], o)) return i;
            }

            return -1;
        }

        public T Remove(int index)
        {
            if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException(nameof(index));
            T removed = elementData[index];
            RemoveAt(index);
            return removed;
        }

        private void RemoveAt(int index)
        {
            for (int i = index; i < elementCount - 1; i++) elementData[i] = elementData[i + 1];
            elementCount--;
            elementData[elementCount] = default!;
        }

        public T Set(int index, T e)
        {
            if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException(nameof(index));
            T old = elementData[index];
            elementData[index] = e;
            return old;
        }

        public MyVector<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > elementCount || fromIndex > toIndex) throw new ArgumentOutOfRangeException();
            int len = toIndex - fromIndex;
            T[] arr = new T[len];
            Array.Copy(elementData, fromIndex, arr, 0, len);
            return new MyVector<T>(arr);
        }

        public T FirstElement()
        {
            if (elementCount == 0) throw new InvalidOperationException("Vector is empty");
            return elementData[0];
        }

        public T LastElement()
        {
            if (elementCount == 0) throw new InvalidOperationException("Vector is empty");
            return elementData[elementCount - 1];
        }

        public void RemoveElementAt(int pos)
        {
            Remove(pos);
        }

        public void RemoveRange(int begin, int end)
        {
            if (begin < 0 || end > elementCount || begin > end) throw new ArgumentOutOfRangeException();
            int num = end - begin;
            for (int i = begin; i < elementCount - num; i++)
                elementData[i] = elementData[i + num];
            for (int i = elementCount - num; i < elementCount; i++)
                elementData[i] = default!;
            elementCount -= num;
        }

        public override string ToString()
        {
            if (elementCount == 0) return "[]";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append('[');
            for (int i = 0; i < elementCount; i++)
            {
                sb.Append(elementData[i]);
                if (i < elementCount - 1) sb.Append(", ");
            }

            sb.Append(']');
            return sb.ToString();
        }
    }
}
    