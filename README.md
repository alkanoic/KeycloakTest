# KeycloakTest

Keycloak.AuthServices.Authenticationを使用する。

## Keycloak設定

### Create Realm

Realm name：Test

Realmを「Test」に設定

### Create User

Users -> Add user

Username：test

testユーザを選択。
Credentialsタブで新しいパスワードを追加。
Passwordをtestとして設定。

### Create Client

- Client type：OpenID Connect
- Client ID test-client

## docker volume作成

mkdir -p ./docker/keycloak/data/import
