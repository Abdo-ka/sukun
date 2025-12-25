import 'package:auto_route/auto_route.dart';
import 'package:flutter/material.dart';
import 'package:core/core.dart';
import 'mobile/home_page_mobile.dart';

@RoutePage()
class HomePage extends StatelessWidget {
static String get name => "HomePage";
static String get path => "HomePage";

const HomePage({super.key});

@override
Widget build(BuildContext context) {
return AppScaffold(
body: PageLayoutBuilder(
mobile: (context) => const HomePageMobile(),
),
);

}

}
