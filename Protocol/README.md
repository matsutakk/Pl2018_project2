## Protocol between Client and Server

### port番号とIPアドレスの組は( port, IP ) = ( 6321, 127.0.0.1 )

### パスワード関連
  1. パスワードの取得
     - サーバは 文字列 "GET_PASSWORD"を受信すると, "UserInfo "+userinfo を送信します.

  2. パスワードの更新
     - サーバは 文字列 "UPDATE_PASSWORD%%変更後のパスワード"を受信すると, パスワードを更新します.

### 友達リスト関連

  1. 友達リストの取得
     - サーバは 文字列 "GET_FRIENDSLIST"を受信すると, "FriendList "+listを送信します.

  2. 友達リストへの友達の追加
     - サーバは 文字列 "INSERT_FRIENDSLIST%%追加する友達の名前"を受信すると, 友達を追加します.

### チャット関連

  1. チャット履歴の取得
     - サーバは文字列 "GET_CHAT_HISTORY%%チャット相手の名前"を受信すると, "ChatRecord "+チャット履歴を送信します.
        
  2. チャット履歴の更新
     - サーバは文字列 "UPDATE_CHAT_HISTORY%%チャット相手の名前%%チャットの内容"を受信すると, チャット履歴を更新します.
  
  3. チャットの保存形式
     - 発言したユーザ名&&チャットの内容::  
  4. メッセージの転送
     - チャット履歴が更新されたら、二つのクライアントに "ChatRecord "+チャット履歴を送信します.


### クイズ関連
  1. クイズ問題と選択肢, 回答の取得
     - サーバは文字列 "GET_QUIZ%%相手の名前"を受信すると, ”Quiz ”+クイズ問題と選択肢, 回答（Quiz&&問題1&&問題1の選択肢1&&問題1の選択肢2&&問題1の選択肢3&&問題1の正解&&問題2&&問題2の選択肢1&&問題2の選択肢2&&問題2の選択肢3&&問題2の正解...）を送信します.
     
  2. クイズ問題と選択肢, 回答の作成
     - サーバは文字列 "CREATE_QUIZ%%相手の名前%%問題__答1__答2__答3__正解"を受信すると, クイズ問題と選択肢, 回答を追加します.
     
  3. クイズ問題と選択肢, 回答の更新
     - サーバは文字列 "UPDATE_QUIZ%%相手の名前%%問題__答1__答2__答3__正解"を受信すると, クイズ問題と選択肢, 回答をすべて上書きします.
