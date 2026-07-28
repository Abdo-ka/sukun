import 'package:flutter/material.dart';

class DismissKeyboardGestureDetectorWrapper extends StatelessWidget {
  final Widget child;

  const DismissKeyboardGestureDetectorWrapper({super.key, required this.child});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () => FocusManager.instance.primaryFocus?.unfocus(),
      child: child,
    );
  }
}
