import 'package:hive/hive.dart';
import 'package:hive_flutter/adapters.dart';
import 'package:injectable/injectable.dart';
import 'package:path_provider/path_provider.dart';

import '../core/config/constants/hive_key.dart';

@lazySingleton
class HiveService {
  static var hive = Hive.box('mobile');
  static String? language;

  static Future<void> initialHive() async {
    Hive.init((await getApplicationCacheDirectory()).path);
    // ..registerAdapter(SignInAdapter());

    await Hive.openBox('mobile');
    language = await getLanguage;
  }

  static Future<dynamic> get getUser async {
    return (await HiveService.hive.get(HiveKey.user));
  }

  static Future<String?> get getLanguage async {
    return (await HiveService.hive.get(HiveKey.language));
  }

  static Future<void> setLanguage(String lang) async {
    await HiveService.hive.put(HiveKey.language, lang);
    language = await getLanguage;
  }

  static Future<bool> get hasSeenOnboarding async {
    return (await HiveService.hive.get(
          HiveKey.hasSeenOnboarding,
        )) ??
        false;
  }

  static Future<void> setHasSeenOnboarding(bool val) async {
    (await HiveService.hive.put(
      HiveKey.hasSeenOnboarding,
      val,
    ));
  }
}
