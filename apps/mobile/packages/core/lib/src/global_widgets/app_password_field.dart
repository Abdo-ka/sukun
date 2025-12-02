import 'package:core/core.dart';
import 'package:flutter/material.dart';

/// Optimized password field with smooth animations
class AppPasswordField extends StatefulWidget {
  final String? name;
  final String? hintText;
  final String? initialValue;
  final String? Function(String?)? validator;

  const AppPasswordField({
    super.key,
    this.name,
    this.hintText,
    this.initialValue,
    this.validator,
  });

  @override
  State<AppPasswordField> createState() => _AppPasswordFieldState();
}

class _AppPasswordFieldState extends State<AppPasswordField> {
  bool _obscureText = true;

  void _toggleVisibility() {
    setState(() => _obscureText = !_obscureText);
  }

  @override
  Widget build(BuildContext context) {
    return AppTextFormField(
      name: widget.name ?? 'password',
      hintText: widget.hintText ?? 'Password',
      obscureText: _obscureText,
      validator: widget.validator,
      initialValue: widget.initialValue,
      suffixIcon: AnimatedSwitcher(
        duration: const Duration(milliseconds: 400),
        transitionBuilder: (Widget child, Animation<double> animation) {
          return FadeTransition(opacity: animation, child: child);
        },
        child: IconButton(
          key: ValueKey<bool>(_obscureText),
          onPressed: _toggleVisibility,
          icon: Icon(
            _obscureText
                ? Icons.visibility_outlined
                : Icons.visibility_off_outlined,
            color: Colors.grey[500],
          ),
        ),
      ),
    );
  }
}
