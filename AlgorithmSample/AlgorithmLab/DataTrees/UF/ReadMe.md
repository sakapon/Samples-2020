# Union-Find
リスト、木構造ともに、前半では ID は十分小さい非負整数とします。

## リスト (Quick Find)
すべての要素が連結されるまでに O(n log n) 時間かかります。  
Find 操作は O(1) 時間で、グループ取得操作もしやすいです。
- 111 基本
- 411 111 + ToGroups, GroupsCount
- 412 111 + Groups, GroupsCount
- 416 typed id、頂点を動的に追加できる
- 516 416 + データ拡張

## 木構造
Union および Find 操作は O(α(n)) 時間であり高速です (301, 302)。  
ただし、グループ内の要素一覧を取得するには別途処理が必要です。
- 001 工夫なし O(n)
- 101 union by size O(log n)
- 102 union by rank O(log n)
- 201 path compression O(log n)
- 301 union by size, path compression O(α(n))
- 302 union by rank, path compression O(α(n))
- 401 301 + ToGroups, GroupsCount
- 402 typed id、頂点を動的に追加できる
- 403 typed id、頂点を静的に登録する
- 501 401 + データ拡張
- 502 402 + データ拡張
