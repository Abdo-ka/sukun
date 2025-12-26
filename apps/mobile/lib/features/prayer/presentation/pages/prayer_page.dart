import 'package:auto_route/auto_route.dart';
import 'package:flutter/material.dart';
import 'package:core/core.dart';
import 'mobile/prayer_page_mobile.dart';

@RoutePage()
class PrayerPage extends StatelessWidget {
static String get name => "PrayerPage";
static String get path => "PrayerPage";

const PrayerPage({super.key});

@override
Widget build(BuildContext context) {
return AppScaffold(
body: PageLayoutBuilder(
mobile: (context) => const PrayerPageMobile(),
),
);

}

}
