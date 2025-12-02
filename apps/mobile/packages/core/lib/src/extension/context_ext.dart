import 'package:flutter/material.dart';

extension ContextExtension on BuildContext {
  double get height => MediaQuery.of(this).size.height;

  double get width => MediaQuery.of(this).size.width;

  LinearGradient get primaryLinear => LinearGradient(
        colors: [
          Theme.of(this).primaryColor,
          Theme.of(this).primaryColor.withOpacity(.5)
        ],
      );

  bool get isDark => Theme.of(this).brightness == Brightness.dark;
}
