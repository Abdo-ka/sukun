import 'package:auto_route/auto_route.dart';
import 'package:flutter/material.dart';
import 'package:core/core.dart';
import 'mobile/{{feature_name.snakeCase()}}_page_mobile.dart';

@RoutePage()
class {{feature_name.pascalCase()}}Page extends StatelessWidget {
static String get name => "{{feature_name.pascalCase()}}Page";
static String get path => "{{feature_name.pascalCase()}}Page";

const {{feature_name.pascalCase()}}Page({super.key});

@override
Widget build(BuildContext context) {
return AppScaffold(
body: PageLayoutBuilder(
mobile: (context) => const {{feature_name.pascalCase()}}PageMobile(),
),
);

}

}
