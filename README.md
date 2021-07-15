# Overview
[LINE API Use Caseサイト](https://lineapiusecase.com/ja/top.html)で提供している[会員証](https://lineapiusecase.com/ja/usecase/membership.html)のデモアプリケーションソースコードとなります。    
今回紹介している手順を参考にすると、LINE APIを活用した会員証アプリケーションを開発することが可能です。  
たとえば、スーパー、ドラッグストア、アパレルなど、オフライン店舗で会員証を提供している企業も、オンライン会員証に移行することができます。  
さらに、会員登録時に店舗の公式アカウントと友達登録を促すことで、ユーザーとのコミュニケーションチャネルを構築できます。  

なお、このページで紹介しているソースコードの環境はMicrosoft Azureを利用しています。  

※ [The English version document is here.](./docs/en/README_en.md)

### 公式ドキュメントの参考箇所
公式ドキュメントの以下の項目を完了させ、次の手順に進んでください。なお、既に導入済みのものは適宜飛ばして下さい。  
※本資料は 2021 年 6 月に作成しているため、最新の公式ドキュメントの内容と齟齬がある可能性があります。

1. [Azure CLI(Bash) のインストール](https://docs.microsoft.com/ja-jp/cli/azure/install-azure-cli)
1. [Azure DevOpsの開始](https://docs.microsoft.com/ja-jp/azure/devops/user-guide/sign-up-invite-teammates?view=azure-devops)

# Getting Started / Tutorial
こちらの手順では、アプリケーション開発に必要な「LINEチャネル作成、アプリケーションデプロイ、バックエンド・フロントエンドの開発環境構築、動作確認」について説明します。
以下リンク先の手順を参考にし、本番環境（Azure）とローカル環境の構築を行ってください。

### [LINE チャネルの作成](./docs/jp/liff-channel-create.md)
### [本番(Azure)環境構築/デプロイ](./docs/jp/deployment.md)
### [バックエンドの構築](./docs/jp/backend-deployment.md)
### [フロントエンド環境構築](./docs/jp/frontend-deployment.md)
***
### [動作確認](./docs/jp/validation.md)
***
# License
MembersCardの全てのファイルは、条件なしで自由にご利用いただけます。
自由にdownload&cloneをして、LINE APIを活用した素敵なアプリケーションの開発を始めてください！
