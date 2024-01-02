# Priority Queue

- 値の多重性
  - 基本的には多重集合
- key のみか、key-value pair か
  - 両方用意することが望ましい

## API
### Constructors
- ctor(items, comparer, descending)

### Basic Operations
- Count
- First
- void Push(item)
  - void Add(item)
- T Pop()
  - bool RemoveFirst()

### Removable
- bool Remove(item)
- int GetCount(item)
  - bool Contains(item)

### Others
- Comparer
- Descending
- RawItems
- void Clear()

## 実用可能な実装
- [PQ202](../PQ202)
