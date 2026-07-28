// 📦 Package imports:
import 'package:injectable/injectable.dart';
import 'package:shared_preferences/shared_preferences.dart';

@singleton
class LocalStorage {
  final SharedPreferences sharedPreferences;

  LocalStorage(this.sharedPreferences);
  bool isFirstOpen(String name) => sharedPreferences.getBool(name) ?? true;
  void setIsFirstOpen(String name, bool value) =>
      sharedPreferences.setBool(name, value);
}
