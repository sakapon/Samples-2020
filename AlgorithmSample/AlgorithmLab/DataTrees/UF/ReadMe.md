## Union-Find
リスト構造、木構造ともに、前半では ID は十分小さい非負整数とします。

### リスト構造 (Quick Find)
すべての要素が連結されるまでに O(n log n) 時間かかります。  
Find 操作は O(1) で、グループ取得操作もしやすいです。
- 111 基本
- 411 111 + ToGroups, GroupsCount
- 412 111 + Groups, GroupsCount
- 416 typed id、頂点を動的に追加できる
- 516 416 + データ拡張

### 木構造
- 001 工夫なし
- 101 union by size
- 102 union by rank
- 201 path compression
- 301 union by size, path compression
- 302 union by rank, path compression
- 401 301 + ToGroups, GroupsCount
- 402 typed id、頂点を動的に追加できる
- 403 typed id、頂点を静的に登録する
- 501 401 + データ拡張
- 502 402 + データ拡張
