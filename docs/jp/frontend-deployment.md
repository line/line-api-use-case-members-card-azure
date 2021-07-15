# Azureへデプロイしたフロントエンドの設定

デプロイはパイプラインで実行されるので特に作業はありません

# 開発環境 / ローカル開発環境
## IDE
お好みのもので構いませんが、特にこだわりがなければVSCodeをお勧めします。

Visual Studio Code  
https://code.visualstudio.com

## PHP, http-server
ローカルサーバーを立てるのに使用します。  

http-server  
https://www.npmjs.com/package/http-server


## ngrok
ローカルのLIFFアプリを表示するためにngrokを使用します。
ngrokのインストール  
https://ngrok.com/download

## ngrokを使ってローカル実行
  - [members_card.js](../../frontend/members_card.js)の環境変数を適切な値に変更
    - `const BASE_URL` をStatic Web AppsのURLに変更する
    - `const liffId` の値はLINE公式アカウントからliffIdを取得し変更する
  - [frontendフォルダ](../../frontend/)直下に移動し、ローカルサーバーをたてる
    - `php -S localhost:5000` or `http-server -p 5000`  など
  - [ngrok](https://ngrok.com/) を使って公開サーバーのURLを発行
      - `ngrok http 5000`
        - LIFFアプリのエンドポイントURLをngrokが発行したURLに設定
  - functionsを起動後、LIFF URLに遷移する ![liff-url](../images/jp/liff-url.png)

[次の頁へ](validation.md)

[目次へ戻る](../../README.md)
