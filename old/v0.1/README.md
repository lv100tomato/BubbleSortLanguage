# BubbleSort言語
バブルソートに着想を得て作りました。いうまでもなくEsolangです。

## 処理
BrainFuckをベースにしているので、それといった文法はありません。この言語においては、仮想的なメモリと、そのメモリへのポインタ2つ(ポインタA,B)を扱うことが出来ます。
改行で区切られた数列をソースコードとして用いることが出来ます。ソースコードを読み込ませて実行すると、数列をバブルソートに従ってソートしていきます。この際、入れ替えが発生すると、入れ替えの処理に続いて以下の処理が行われます。

    入れ替えた二つの値の合計を32で割った余りにについて、
    - 0のとき:ポインタAをインクリメントする
    - 1のとき:ポインタBをインクリメントする
    - 2のとき:ポインタAをデクリメントする
    - 3のとき:ポインタBをデクリメントする
    - 4のとき:ポインタAのさすメモリの値をインクリメントする
    - 5のとき:ポインタBのさすメモリの値をインクリメントする
    - 6のとき:ポインタAのさすメモリの値をデクリメントする
    - 7のとき:ポインタBのさすメモリの値をデクリメントする
    - 8のとき:ポインタAのさすメモリの値を出力する
    - 9のとき:ポインタBのさすメモリの値を出力する
    - 10のとき:入力から1バイト読んでポインタAのさすメモリに代入する
    - 11のとき:入力から1バイト読んでポインタBのさすメモリに代入する
    - 12のとき:入れ替えた二つの数のうち小さいほうのインデックスにポインタAのさす値の数を足したインデックスの数に、ポインタBのさす値を足す
    - 13のとき:入れ替えた二つの数のうち小さいほうのインデックスにポインタAのさす値の数を足したインデックスの数に、ポインタBのさす値を引く
    - 上記以外:何もしない

## 実行方法
ソースコードを実行ファイルと同じフォルダに置き、引数にソースコードのファイル名を与えてることで実行できます。
引数を指定しなかった場合は、直接ソースコードを書いて実行することもできます。

## サンプルコード
サンプルコードとして、hello_world.bslとcat.bslを用意しました。プログラミングする際の参考にしてください。

    - hello_world.bsl : `Hello, World!`と出力します。
    - cat.bsl : 入力した文字列を出力します。出力し終わると入力フェーズに戻ります。終了する際はCtrl+C等で強制終了させてください。
