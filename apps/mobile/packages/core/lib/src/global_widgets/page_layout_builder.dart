import 'package:flutter/material.dart';
import 'package:responsive_builder/responsive_builder.dart' hide WidgetBuilder;

class PageLayoutBuilder extends StatelessWidget {
  const PageLayoutBuilder({required this.mobile, super.key, this.desktop});

  final WidgetBuilder mobile;
  final WidgetBuilder? desktop;

  @override
  Widget build(BuildContext context) {
    return ScreenTypeLayout.builder(desktop: desktop ?? mobile, mobile: mobile);
  }
}
