// 📦 Package imports:
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:injectable/injectable.dart';
import 'package:jwt_decoder/jwt_decoder.dart';

abstract class TokenRepository {
  Future<String?> get token;
  Future<void> saveToken(String token);
  Future<void> deleteToken();
  Future<bool> isTokenExpired([String? token]);
  Future<bool> hastToken();
}

@Injectable(as: TokenRepository)
class TokenRepositoryImp implements TokenRepository {
  final FlutterSecureStorage secureStorage = const FlutterSecureStorage();
  final String tokenKey = 'token';
  @override
  Future<void> deleteToken() async => secureStorage.delete(key: tokenKey);

  @override
  Future<bool> isTokenExpired([String? token]) async {
    final String? accessToken = token ?? await this.token;

    if (accessToken == null || accessToken.isEmpty) return false;

    return JwtDecoder.isExpired(accessToken);
  }

  @override
  Future<void> saveToken(String token) =>
      secureStorage.write(key: tokenKey, value: token);

  @override
  Future<String?> get token => secureStorage.read(key: tokenKey);

  @override
  Future<bool> hastToken() async {
    final String? token = await this.token;
    return token != null && token.isNotEmpty;
  }
}
