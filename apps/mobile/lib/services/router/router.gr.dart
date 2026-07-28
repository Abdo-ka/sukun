// dart format width=80
// GENERATED CODE - DO NOT MODIFY BY HAND

// **************************************************************************
// AutoRouterGenerator
// **************************************************************************

// ignore_for_file: type=lint
// coverage:ignore-file

// ignore_for_file: no_leading_underscores_for_library_prefixes
import 'package:auto_route/auto_route.dart' as _i7;
import 'package:flutter/material.dart' as _i8;
import 'package:mobile/features/home/presentation/pages/home_page.dart' as _i1;
import 'package:mobile/features/home/presentation/pages/mobile/home_page_mobile.dart'
    as _i2;
import 'package:mobile/features/others/presentation/pages/others_details_page.dart'
    as _i3;
import 'package:mobile/features/others/presentation/pages/others_page.dart'
    as _i4;
import 'package:mobile/features/prayer/presentation/pages/mobile/setting_pray_page.dart'
    as _i6;
import 'package:mobile/features/prayer/presentation/pages/prayer_page.dart'
    as _i5;

/// generated route for
/// [_i1.HomePage]
class HomeRoute extends _i7.PageRouteInfo<void> {
  const HomeRoute({List<_i7.PageRouteInfo>? children})
    : super(HomeRoute.name, initialChildren: children);

  static const String name = 'HomeRoute';

  static _i7.PageInfo page = _i7.PageInfo(
    name,
    builder: (data) {
      return const _i1.HomePage();
    },
  );
}

/// generated route for
/// [_i2.HomePageMobile]
class HomeRouteMobile extends _i7.PageRouteInfo<void> {
  const HomeRouteMobile({List<_i7.PageRouteInfo>? children})
    : super(HomeRouteMobile.name, initialChildren: children);

  static const String name = 'HomeRouteMobile';

  static _i7.PageInfo page = _i7.PageInfo(
    name,
    builder: (data) {
      return const _i2.HomePageMobile();
    },
  );
}

/// generated route for
/// [_i3.OthersDetailsPage]
class OthersDetailsRoute extends _i7.PageRouteInfo<OthersDetailsRouteArgs> {
  OthersDetailsRoute({
    _i8.Key? key,
    required int categoryId,
    required String title,
    required dynamic icon,
    List<_i7.PageRouteInfo>? children,
  }) : super(
         OthersDetailsRoute.name,
         args: OthersDetailsRouteArgs(
           key: key,
           categoryId: categoryId,
           title: title,
           icon: icon,
         ),
         initialChildren: children,
       );

  static const String name = 'OthersDetailsRoute';

  static _i7.PageInfo page = _i7.PageInfo(
    name,
    builder: (data) {
      final args = data.argsAs<OthersDetailsRouteArgs>();
      return _i3.OthersDetailsPage(
        key: args.key,
        categoryId: args.categoryId,
        title: args.title,
        icon: args.icon,
      );
    },
  );
}

class OthersDetailsRouteArgs {
  const OthersDetailsRouteArgs({
    this.key,
    required this.categoryId,
    required this.title,
    required this.icon,
  });

  final _i8.Key? key;

  final int categoryId;

  final String title;

  final dynamic icon;

  @override
  String toString() {
    return 'OthersDetailsRouteArgs{key: $key, categoryId: $categoryId, title: $title, icon: $icon}';
  }

  @override
  bool operator ==(Object other) {
    if (identical(this, other)) return true;
    if (other is! OthersDetailsRouteArgs) return false;
    return key == other.key &&
        categoryId == other.categoryId &&
        title == other.title &&
        icon == other.icon;
  }

  @override
  int get hashCode =>
      key.hashCode ^ categoryId.hashCode ^ title.hashCode ^ icon.hashCode;
}

/// generated route for
/// [_i4.OthersPage]
class OthersRoute extends _i7.PageRouteInfo<void> {
  const OthersRoute({List<_i7.PageRouteInfo>? children})
    : super(OthersRoute.name, initialChildren: children);

  static const String name = 'OthersRoute';

  static _i7.PageInfo page = _i7.PageInfo(
    name,
    builder: (data) {
      return const _i4.OthersPage();
    },
  );
}

/// generated route for
/// [_i5.PrayerPage]
class PrayerRoute extends _i7.PageRouteInfo<void> {
  const PrayerRoute({List<_i7.PageRouteInfo>? children})
    : super(PrayerRoute.name, initialChildren: children);

  static const String name = 'PrayerRoute';

  static _i7.PageInfo page = _i7.PageInfo(
    name,
    builder: (data) {
      return const _i5.PrayerPage();
    },
  );
}

/// generated route for
/// [_i6.SettingPrayPage]
class SettingPrayRoute extends _i7.PageRouteInfo<void> {
  const SettingPrayRoute({List<_i7.PageRouteInfo>? children})
    : super(SettingPrayRoute.name, initialChildren: children);

  static const String name = 'SettingPrayRoute';

  static _i7.PageInfo page = _i7.PageInfo(
    name,
    builder: (data) {
      return const _i6.SettingPrayPage();
    },
  );
}
