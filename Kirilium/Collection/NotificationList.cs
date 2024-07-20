using System;
using System.Collections;
using System.Collections.Generic;

namespace Kirilium.Collection
{
    public class NotificationList<T> : IEnumerable<T>, IList<T>
    {
        // 非公開フィールド
        private readonly List<T> values;

        // イベント
        internal event EventHandler ValueCollectionChanged;

        // コンストラクタ
        public NotificationList()
        {
            this.values = new List<T>();
        }

        #region プロパティ

        /// <summary>
        /// タブページ数
        /// </summary>
        public int Count
        {
            get
            {
                return this.values.Count;
            }
        }

        /// <summary>
        /// 読み取り専用であるかどうかを示す。
        /// </summary>
        public bool IsReadOnly { set; get; }

        public T this[int index]
        {
            get
            {
                return this.values[index];
            }
            set
            {
                this.values[index] = value;
            }
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            return this.values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.values.GetEnumerator();
        }

        /// <summary>
        /// 指定されたアイテムが最初に出現するインデックスを取得する。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            return this.values.IndexOf(item);
        }

        /// <summary>
        /// 指定されたインデックスにアイテムを追加する。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            if (this.IsReadOnly)
            {
                return;
            }

            this.values.Insert(index, item);
            OnValueCollectionChanged();
        }

        /// <summary>
        /// 指定されたインデックスのアイテムをコレクションから削除する。
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (this.IsReadOnly)
            {
                return;
            }

            this.values.RemoveAt(index);
            OnValueCollectionChanged();
        }

        /// <summary>
        /// コレクションの末尾にアイテムを追加する。
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (this.IsReadOnly)
            {
                return;
            }

            this.values.Add(item);
            OnValueCollectionChanged();
        }

        /// <summary>
        /// コレクションに含まれるタブページをすべて削除する。
        /// </summary>
        public void Clear()
        {
            if (this.IsReadOnly)
            {
                return;
            }

            this.values.Clear();
            OnValueCollectionChanged();
        }

        /// <summary>
        /// このコレクションに、指定されたアイテムが含まれているかを判定する。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return this.values.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (this.IsReadOnly)
            {
                return;
            }

            this.values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// このコレクションから指定されたアイテムを削除する。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            if (this.IsReadOnly)
            {
                return false;
            }

            this.values.Remove(item);
            OnValueCollectionChanged();

            return true;
        }

        /// <summary>
        /// 値が変更された場合の処理
        /// </summary>
        protected virtual void OnValueCollectionChanged()
        {
            this.ValueCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
