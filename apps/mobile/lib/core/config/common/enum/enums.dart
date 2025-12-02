enum StatusCode {
  operationSucceeded(200),
  operationFailed(400),
  unAuthorized(401),
  forbidden(403),
  serverError(500),
  ;

  const StatusCode(this.code);

  final int code;
}

enum Status { initial, loading, success, failure, empty }

enum Flavor {
  Dev,
  Prod,
  Stag;

  bool get isDev => this == Flavor.Dev;
  bool get isProd => this == Flavor.Prod;
  bool get isStag => this == Flavor.Stag;
}
