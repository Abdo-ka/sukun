// import 'package:core/core.dart';
// import 'package:core/src/theme/extension_color_scheme.dart';
// import 'package:flutter/material.dart';
// import 'package:flutter_screenutil/flutter_screenutil.dart';

// ///enum
// enum ButtonSize {
//   ///56
//   xl,

//   ///48
//   large,

//   ///40
//   medium,

//   ///32
//   small
// }

// enum FocusedFilledButtonType {
//   border,
//   hideBackgroundColor,

//   ///this is implemented only on [AppButtonType.tertiary]
//   ///TODO :implement on other type
//   hover,
//   none
// }

// enum AppButtonType {
//   field,
//   secondary,
//   tertiary,
//   ghost,
//   light,
//   dark,
//   gray,
//   payment,
//   oldPayment
// }

// ///Type NamedConstructor
// class AppButton extends StatefulWidget {
//   const AppButton.light({
//     this.textStyle,
//     this.buttonSize = ButtonSize.large,
//     this.onPressed,
//     this.child,
//     this.mainAxisAlignment,
//     this.title,
//     this.borderRadius,
//     this.padding,
//     this.stretch = false,
//     super.key,
//     this.style,
//     this.fixedSize,
//     this.margin,
//   })  : _typeButton = AppButtonType.light,
//         spaceBetweenItem = 0,
//         postfixIcon = null,
//         prefixIcon = null,
//         focusedFilledType = FocusedFilledButtonType.none;

//   const AppButton.dark({
//     this.textStyle,
//     this.buttonSize = ButtonSize.large,
//     this.onPressed,
//     this.child,
//     this.stretch = false,
//     this.mainAxisAlignment,
//     this.title,
//     this.borderRadius,
//     this.padding,
//     super.key,
//     this.style,
//     this.fixedSize,
//     this.margin,
//   })  : _typeButton = AppButtonType.dark,
//         spaceBetweenItem = 0,
//         postfixIcon = null,
//         prefixIcon = null,
//         focusedFilledType = FocusedFilledButtonType.none;

//   const AppButton.gray({
//     this.textStyle,
//     this.buttonSize = ButtonSize.large,
//     this.onPressed,
//     this.child,
//     this.mainAxisAlignment,
//     this.title,
//     this.borderRadius,
//     this.padding,
//     super.key,
//     this.style,
//     this.fixedSize,
//     this.stretch = false,
//     this.margin,
//   })  : _typeButton = AppButtonType.gray,
//         spaceBetweenItem = 0,
//         postfixIcon = null,
//         prefixIcon = null,
//         focusedFilledType = FocusedFilledButtonType.none;

//   AppButton.payment({
//     this.textStyle,
//     this.buttonSize = ButtonSize.large,
//     this.onPressed,
//     this.child,
//     this.stretch = false,
//     this.borderRadius,
//     required String leadingTitle,
//     required String total,
//     this.padding,
//     super.key,
//     this.style,
//     this.fixedSize,
//     this.margin,
//   })  : title = "",
//         _typeButton = AppButtonType.payment,
//         spaceBetweenItem = 0,
//         mainAxisAlignment = MainAxisAlignment.start,
//         prefixIcon = AppText.bodySmall(
//           fontWeight: FontWeight.w500,
//           leadingTitle,
//           textAlign: TextAlign.start,
//         ),
//         postfixIcon = AppText.bodyMedium(
//           total,
//           textAlign: TextAlign.end,
//         ),
//         focusedFilledType = FocusedFilledButtonType.none;

//   AppButton.oldPayment({
//     this.textStyle,
//     this.buttonSize = ButtonSize.large,
//     this.onPressed,
//     this.child,
//     this.stretch = false,
//     this.borderRadius,
//     required String leadingTitle,
//     required String total,
//     this.padding,
//     super.key,
//     this.style,
//     this.fixedSize,
//     this.margin,
//   })  : title = "",
//         _typeButton = AppButtonType.oldPayment,
//         spaceBetweenItem = 0,
//         mainAxisAlignment = MainAxisAlignment.start,
//         prefixIcon = AppText.bodySmall(
//           fontWeight: FontWeight.w500,
//           leadingTitle,
//           textAlign: TextAlign.start,
//         ),
//         postfixIcon = AppText.bodyMedium(
//           total,
//           textAlign: TextAlign.center,

//           ///TODO GET this color from theme
//           color: const Color(0xff222636),
//         ),
//         focusedFilledType = FocusedFilledButtonType.none;

//   const AppButton.field({
//     this.textStyle,
//     this.buttonSize = ButtonSize.medium,
//     this.onPressed,
//     this.child,
//     this.mainAxisAlignment,
//     this.title,
//     this.borderRadius,
//     this.padding,
//     this.stretch = false,
//     this.prefixIcon,
//     this.postfixIcon,
//     super.key,
//     this.spaceBetweenItem = 8,
//     this.style,
//     this.fixedSize,
//     this.focusedFilledType = FocusedFilledButtonType.border,
//     this.margin,
//   }) : _typeButton = AppButtonType.field;

//   const AppButton.secondary({
//     this.textStyle,
//     this.buttonSize = ButtonSize.medium,
//     this.onPressed,
//     this.child,
//     this.title,
//     this.stretch = false,
//     this.borderRadius,
//     this.prefixIcon,
//     this.mainAxisAlignment,
//     this.postfixIcon,
//     super.key,
//     this.spaceBetweenItem = 8,
//     this.padding,
//     this.style,
//     this.fixedSize,
//     this.margin,
//   })  : _typeButton = AppButtonType.secondary,
//         focusedFilledType = FocusedFilledButtonType.none;

//   const AppButton.tertiary({
//     this.textStyle,
//     this.stretch = false,
//     this.buttonSize = ButtonSize.medium,
//     this.onPressed,
//     this.borderRadius,
//     this.child,
//     this.title,
//     this.spaceBetweenItem = 8,
//     this.mainAxisAlignment,
//     this.prefixIcon,
//     this.postfixIcon,
//     this.padding,
//     super.key,
//     this.style,
//     this.fixedSize,
//     this.focusedFilledType = FocusedFilledButtonType.none,
//     this.margin,
//   }) : _typeButton = AppButtonType.tertiary;

//   const AppButton.ghost({
//     this.textStyle,
//     this.buttonSize = ButtonSize.medium,
//     this.onPressed,
//     this.borderRadius,
//     this.stretch = false,
//     this.child,
//     this.padding,
//     this.spaceBetweenItem = 8,
//     this.title,
//     this.mainAxisAlignment,
//     super.key,
//     this.prefixIcon,
//     this.postfixIcon,
//     this.style,
//     this.fixedSize,
//     this.margin,
//   })  : _typeButton = AppButtonType.ghost,
//         focusedFilledType = FocusedFilledButtonType.none;

//   ///Button Parameter
//   final VoidCallback? onPressed;
//   final ButtonStyle? style;

//   ///Custom Parameter
//   final BorderRadiusGeometry? borderRadius;
//   final ButtonSize buttonSize;
//   final Widget? child;
//   final Widget? prefixIcon;
//   final Widget? postfixIcon;
//   final String? title;
//   final TextStyle? textStyle;
//   final AppButtonType _typeButton;
//   final EdgeInsetsGeometry? padding;
//   final EdgeInsets? margin;
//   final double spaceBetweenItem;
//   final FocusedFilledButtonType focusedFilledType;

//   ///this will make button take all space
//   ///TODO TEST IT ON ALL BUTTON TYPE
//   final bool stretch;

//   ///pass this will ignore [buttonSize]
//   final Size? fixedSize;

//   ///this will be ignore when the [postfixIcon] && [prefixIcon] is null
//   final MainAxisAlignment? mainAxisAlignment;

//   @override
//   State<AppButton> createState() => _AppButtonState();
// }

// class _AppButtonState extends State<AppButton> {
//   late final ValueNotifier<Color?>
//       _overlayPostPrefixIconColor;
//   late final ValueNotifier<Color?> _overlayTextColor;

//   @override
//   void initState() {
//     super.initState();
//     _overlayPostPrefixIconColor =
//         ValueNotifier<Color?>(getIconColor);
//     _overlayTextColor = ValueNotifier<Color?>(null);
//   }

//   @override
//   Widget build(BuildContext context) {
//     assert(widget.child != null || widget.title != null);
//     // assert(
//     //     (widget.mainAxisAlignment != null &&
//     //             (widget.prefixIcon == null || widget.postfixIcon == null)) ||
//     //         (widget.mainAxisAlignment == null),
//     //     "you should pass either [prefixIcon] or [postfixIcon] when pass [mainAxisAlignment]");
//     switch (widget._typeButton) {
//       case AppButtonType.secondary:
//       case AppButtonType.field:
//         return GestureDetector(
//           onTapDown: onTapDown,
//           onTapUp: onTapUp,
//           onTapCancel: () => onTapUp(null),
//           child: Padding(
//             padding: getMargin,
//             child: ElevatedButton(
//               onPressed: widget.onPressed,
//               style: getButtonStyle(context),
//               child: getChildButton(context),
//             ),
//           ),
//         );
//       case AppButtonType.tertiary:
//         return GestureDetector(
//           onTapDown: onTapDown,
//           onTapUp: onTapUp,
//           onTapCancel: () => onTapUp(null),
//           child: Padding(
//             padding: getMargin,
//             child: OutlinedButton(
//               onPressed: widget.onPressed,
//               style: getButtonStyle(context),
//               child: getChildButton(context),
//             ),
//           ),
//         );
//       case AppButtonType.ghost:
//         return Padding(
//           padding: getMargin,
//           child: TextButton(
//             onPressed: widget.onPressed,
//             style: getButtonStyle(context),
//             child: getChildButton(context),
//           ),
//         );
//       case AppButtonType.oldPayment:
//         return Container(
//           height: sizeButton.height,
//           margin: getMargin,
//           clipBehavior: Clip.antiAlias,
//           decoration: BoxDecoration(
//             borderRadius: getBorderRadius,
//             border: Border.all(
//               width: 1.r,
//               color:
//                   context.colorScheme.systemGray.shade200,
//             ),
//           ),
//           child: ElevatedButton(
//             onPressed: widget.onPressed,
//             style: getButtonStyle(context),
//             child: getChildButton(context),
//           ),
//         );
//       case AppButtonType.light:
//       case AppButtonType.dark:
//       case AppButtonType.gray:
//       case AppButtonType.payment:
//         return Container(
//           height: sizeButton.height,
//           margin: getMargin,
//           decoration: BoxDecoration(
//             borderRadius: getBorderRadius,
//             boxShadow: [
//               BoxShadow(
//                 offset: const Offset(0, 1),
//                 blurRadius: 2.r,
//                 color: const Color(0x0D101828),
//               ),
//               BoxShadow(
//                 offset: const Offset(0, 4),
//                 blurRadius: 4.r,
//                 color: const Color(0x29FFFFFF),
//               ),
//             ],
//             color: _isGray ? const Color(0xffF9FAFB) : null,
//             border: getSide({}) != null
//                 ? Border.fromBorderSide(getSide({})!)
//                 : null,
//             gradient: _isGray
//                 ? null
//                 : LinearGradient(
//                     begin: AlignmentDirectional.centerStart,
//                     end: AlignmentDirectional.centerEnd,
//                     colors: getGradientsColor),
//           ),
//           child: ElevatedButton(
//             onPressed: widget.onPressed,
//             style: getButtonStyle(context),
//             child: getChildButton(context),
//           ),
//         );
//     }
//   }

//   List<Color> get getGradientsColor {
//     if (_isLight) {
//       return [_brandPrimary[25]!, const Color(0xE0FFF3EE)];
//     }
//     if (_isDark || _isPayment) {
//       return [
//         _brandPrimary.shade200,
//         const Color(0xE0FB5607)
//       ];
//     }
//     return [];
//   }

//   void onTapUp(_) {
//     if (_isTertiary &&
//         widget.focusedFilledType ==
//             FocusedFilledButtonType.hover) {
//       _overlayTextColor.value = _oldTextColor;

//       _overlayPostPrefixIconColor.value = _oldIconColor;
//     } else if (_isTertiary) {
//       _overlayPostPrefixIconColor.value = _oldIconColor;
//     }
//     if (widget._typeButton == AppButtonType.field &&
//         widget.focusedFilledType ==
//             FocusedFilledButtonType.hideBackgroundColor) {
//       _overlayPostPrefixIconColor.value =
//           context.colorScheme.surface;
//     }
//   }

//   ///save postfix icon color to use it when change color if icon when focused
//   Color? _oldIconColor;
//   Color? _oldTextColor;

//   void onTapDown(_) {
//     if (_isTertiary &&
//         widget.focusedFilledType ==
//             FocusedFilledButtonType.hover) {
//       _overlayTextColor.value =
//           context.colorScheme.onPrimary;
//       _overlayPostPrefixIconColor.value =
//           context.colorScheme.onPrimary;
//     } else if (_isTertiary) {
//       _overlayPostPrefixIconColor.value =
//           context.colorScheme.onSurface;
//     }
//     if (widget._typeButton == AppButtonType.field &&
//         widget.focusedFilledType ==
//             FocusedFilledButtonType.hideBackgroundColor) {
//       _overlayPostPrefixIconColor.value =
//           context.colorScheme.onSurface;
//     }
//   }

//   EdgeInsets get getMargin =>
//       widget.margin ?? EdgeInsets.zero;

//   bool get isDisabled => widget.onPressed != null;

//   Color? get backGroundColor {
//     ///if the type is secondary should pass only color

//     return null;
//   }

//   bool get hasIcon =>
//       widget.prefixIcon != null ||
//       widget.postfixIcon != null;

//   Widget getChildButton(BuildContext context) {
//     final Widget buttonChild = widget.child ??
//         AppText(
//           widget.title!,
//           style: getTextStyle(context),
//           maxLines: 1,
//           overflow: TextOverflow.ellipsis,
//         );
//     if (_isPayment || _isOldPayment) {
//       return LayoutBuilder(
//         builder: (context, constraints) {
//           if (_isOldPayment) {
//             return Directionality(
//               textDirection: TextDirection.ltr,
//               child: Row(
//                 crossAxisAlignment:
//                     CrossAxisAlignment.stretch,
//                 children: [
//                   Expanded(
//                     flex: 6,
//                     child: Container(
//                       padding: getPadding,
//                       decoration: BoxDecoration(
//                         borderRadius: BorderRadius.only(
//                           topLeft: Radius.circular(8.r),
//                           bottomLeft: Radius.circular(8.r),
//                         ),
//                         color: context.colorScheme.primary,
//                       ),
//                       alignment:
//                           AlignmentDirectional.centerStart,
//                       child: widget.prefixIcon!,
//                     ),
//                   ),
//                   Expanded(
//                     flex: 4,
//                     child: Container(
//                       padding: getPadding,
//                       alignment:
//                           AlignmentDirectional.center,
//                       decoration: BoxDecoration(
//                         borderRadius: BorderRadius.only(
//                           topRight: Radius.circular(8.r),
//                           bottomRight: Radius.circular(8.r),
//                         ),
//                         color: context.colorScheme.surface,
//                       ),
//                       child: widget.postfixIcon!,
//                     ),
//                   ),
//                 ],
//               ),
//             );
//           }
//           return Row(
//             mainAxisAlignment:
//                 MainAxisAlignment.spaceBetween,
//             children: [
//               Container(
//                   constraints: BoxConstraints(
//                       maxWidth: constraints.maxWidth * 0.5),
//                   child: widget.prefixIcon!),
//               Expanded(child: widget.postfixIcon!),
//             ],
//           );
//         },
//       );
//     }
//     if (hasIcon) {
//       //todo separate postfix icon and prefix icon to  different color
//       if (widget.postfixIcon is AppImage &&
//           _oldIconColor == null) {
//         _oldIconColor =
//             (widget.postfixIcon as AppImage).color;
//       }
//       if (buttonChild is AppText && _oldTextColor == null) {
//         _oldTextColor = (buttonChild).color;
//       }
//       return ValueListenableBuilder(
//           valueListenable: _overlayPostPrefixIconColor,
//           builder: (context, val, child) {
//             return Row(
//               mainAxisSize: widget.stretch
//                   ? MainAxisSize.max
//                   : MainAxisSize.min,
//               mainAxisAlignment: widget.mainAxisAlignment ??
//                   MainAxisAlignment.center,
//               children: [
//                 if (widget.prefixIcon != null) ...[
//                   if (widget.spaceBetweenItem != 0)
//                     widget.spaceBetweenItem.horizontalSpace,
//                   if ((_isTertiary || _isField) &&
//                       widget.prefixIcon is AppImage)
//                     (widget.prefixIcon! as AppImage)
//                         .copyWith(val)
//                   else
//                     widget.prefixIcon!
//                 ],
//                 if (!_isPayment) ...{
//                   if (_isTertiary &&
//                       widget.focusedFilledType ==
//                           FocusedFilledButtonType.hover &&
//                       buttonChild is AppText)
//                     buttonChild.copyWith(
//                         color: _overlayTextColor.value)
//                   else
//                     buttonChild
//                 },
//                 if (widget.postfixIcon != null) ...[
//                   if (widget.spaceBetweenItem != 0)
//                     widget.spaceBetweenItem.horizontalSpace,
//                   if ((_isTertiary || _isField) &&
//                       widget.postfixIcon is AppImage)
//                     (widget.postfixIcon! as AppImage)
//                         .copyWith(val)
//                   else
//                     widget.postfixIcon!
//                 ]
//               ],
//             );
//           });
//     }
//     return buttonChild;
//   }

//   TextStyle getTextStyle(BuildContext context) {
//     if (_isLongerButton) {
//       if (_isDark) {
//         return context.textTheme.body.semiBold;
//       }
//       return context.textTheme.body.medium;
//     }
//     return textTheme.buttonText
//         .merge(widget.textStyle ?? const TextStyle());
//   }

//   bool get _isTertiary =>
//       widget._typeButton == AppButtonType.tertiary;

//   bool get _isField =>
//       widget._typeButton == AppButtonType.field;

//   bool get _isLight =>
//       widget._typeButton == AppButtonType.light;

//   bool get _isDark =>
//       widget._typeButton == AppButtonType.dark;

//   bool get _isGray =>
//       widget._typeButton == AppButtonType.gray;

//   bool get _isPayment =>
//       widget._typeButton == AppButtonType.payment;

//   bool get _isOldPayment =>
//       widget._typeButton == AppButtonType.oldPayment;

//   bool get _isLongerButton =>
//       widget._typeButton == AppButtonType.light ||
//       widget._typeButton == AppButtonType.dark ||
//       widget._typeButton == AppButtonType.payment ||
//       widget._typeButton == AppButtonType.gray;

//   Color? getEnabledColor(BuildContext context) {
//     if (widget._typeButton == AppButtonType.field) {
//       return context.colorScheme.onPrimary;
//     }
//     if (widget._typeButton == AppButtonType.secondary) {
//       return context.colorScheme.onSurface;
//     }
//     if (_isTertiary) {
//       return context.colorScheme.onSurface;
//     }
//     if (widget._typeButton == AppButtonType.ghost) {
//       return context.colorScheme.onSurface;
//     }
//     return null;
//   }

//   Size get sizeButton {
//     if (widget.fixedSize != null) return widget.fixedSize!;

//     switch (widget.buttonSize) {
//       case ButtonSize.xl:
//         return Size.fromHeight(56.h);
//       case ButtonSize.large:
//         return Size.fromHeight(48.h);
//       case ButtonSize.medium:
//         return Size.fromHeight(40.h);
//       case ButtonSize.small:
//         return Size.fromHeight(32.h);
//     }
//   }

//   BorderSide? getSide(Set<WidgetState> states) {
//     if (_isTertiary &&
//         states.contains(WidgetState.pressed) &&
//         widget.focusedFilledType ==
//             FocusedFilledButtonType.hover) {
//       return BorderSide.none;
//     } else if (_isTertiary &&
//         states.contains(WidgetState.pressed)) {
//       return BorderSide(
//         color: context.colorScheme.onSurface,
//         width: 2.r,
//       );
//     }
//     if (_isTertiary && !isDisabled) {
//       ///todo change this
//       return BorderSide(color: Colors.grey.shade300);
//     }

//     if (widget._typeButton == AppButtonType.field &&
//         // widget.focusedFilledType == FocusedFilledButtonType.border &&
//         states.contains(WidgetState.pressed)) {
//       return BorderSide(
//         color: context.colorScheme.onSurface,
//         width: 2.r,
//       );
//     }
//     if (_isLight) {
//       return BorderSide(
//           color: _brandPrimary.shade50, width: 1.r);
//     }

//     if (_isGray) {
//       return BorderSide(
//           color: _systemGray.shade100, width: 1.r);
//     }
//     return null;
//   }

//   ButtonStyle getButtonStyle(BuildContext context) {
//     ButtonStyle buttonStyle = widget.style ??
//         ButtonStyle(
//             backgroundColor:
//                 WidgetStateProperty.resolveWith((states) {
//               var pressed =
//                   states.contains(WidgetState.pressed);
//               if (_isField &&
//                   pressed &&
//                   widget.focusedFilledType ==
//                       FocusedFilledButtonType
//                           .hideBackgroundColor) {
//                 return context.colorScheme.surface;
//               }
//               if (_isField) {
//                 return context.colorScheme.primary;
//               }
//               if (_isTertiary &&
//                   pressed &&
//                   widget.focusedFilledType ==
//                       FocusedFilledButtonType.hover) {
//                 return context.colorScheme.primary;
//               }

//               if (_isTertiary && pressed) {
//                 return context.colorScheme.surface;
//               }
//               if (_isLight || _isDark || _isPayment) {
//                 return Colors.transparent;
//               }
//               if (_isGray) {
//                 return const Color(0xffF9FAFB);
//               }
//               if (_isOldPayment) {
//                 return Colors.transparent;
//               }
//               return null;
//             }),
//             shape: _isLongerButton
//                 ? null
//                 : WidgetStateProperty.resolveWith((states) {
//                     return getShape(states);
//                   }),
//             overlayColor:
//                 WidgetStateProperty.resolveWith((states) {
//               var selected =
//                   states.contains(WidgetState.pressed);
//               if (selected &&
//                   _isTertiary &&
//                   widget.focusedFilledType ==
//                       FocusedFilledButtonType.hover) {
//                 return context.colorScheme.primary;
//               }

//               if (selected && _isTertiary) {
//                 return context.colorScheme.surface;
//               }
//               if (_isField &&
//                   widget.focusedFilledType ==
//                       FocusedFilledButtonType
//                           .hideBackgroundColor) {
//                 return context.colorScheme.surface;
//               }

//               if (_isLight ||
//                   _isDark ||
//                   _isGray ||
//                   _isPayment ||
//                   _isOldPayment) {
//                 return Colors.transparent;
//               }
//               return null;
//             }),
//             shadowColor:
//                 WidgetStateProperty.all(Colors.transparent),
//             side: _isLongerButton
//                 ? null
//                 : WidgetStateProperty.resolveWith(
//                     (states) => getSide(states)),
//             foregroundColor:
//                 WidgetStateProperty.resolveWith((states) {
//               ///to text
//               var pressed =
//                   states.contains(WidgetState.pressed);
//               if (pressed &&
//                   _isField &&
//                   widget.focusedFilledType ==
//                       FocusedFilledButtonType
//                           .hideBackgroundColor) {
//                 return context.colorScheme.onSurface;
//               }
//               if (pressed &&
//                   _isTertiary &&
//                   widget.focusedFilledType ==
//                       FocusedFilledButtonType.hover) {
//                 return context.colorScheme.onPrimary;
//               }

//               if (pressed && _isTertiary) {
//                 return context.colorScheme.onSurface;
//               }

//               if (_isLight && pressed) {
//                 return _brandPrimary.shade100;
//               }
//               if (_isLight) {
//                 return _brandPrimary.shade300;
//               }
//               if (pressed && _isDark) {
//                 return _brandPrimary.shade100;
//               }

//               if (_isDark) {
//                 return context.colorScheme.surface;
//               }
//               if (pressed && _isGray) {
//                 return _systemGray.shade400;
//               }
//               if (_isGray) {
//                 return _systemGray.shade900;
//               }

//               if (pressed && _isPayment) {
//                 return _brandPrimary.shade100;
//               }
//               if (_isPayment) {
//                 return context.colorScheme.surface;
//               }

//               return getEnabledColor(context);
//             }),
//             surfaceTintColor:
//                 WidgetStateProperty.resolveWith((states) {
//               var pressed =
//                   states.contains(WidgetState.pressed);
//               if (_isLongerButton && pressed) {
//                 return Colors.transparent;
//               }
//               return null;
//             }));

//     switch (widget._typeButton) {
//       case AppButtonType.field:
//         return buttonStyle.merge(ElevatedButton.styleFrom(
//           minimumSize: Size.zero,
//           elevation: 0,
//           tapTargetSize: MaterialTapTargetSize.shrinkWrap,
//           fixedSize: sizeButton,
//           padding: getPadding,
//         ));
//       case AppButtonType.secondary:
//         return buttonStyle.merge(ElevatedButton.styleFrom(
//           fixedSize: sizeButton,
//           backgroundColor: context.colorScheme.secondary,
//           elevation: 0,
//           padding: getPadding,
//         ));
//       case AppButtonType.tertiary:
//         return buttonStyle.merge(OutlinedButton.styleFrom(
//           fixedSize: sizeButton,
//           minimumSize: Size.zero,
//           tapTargetSize: MaterialTapTargetSize.shrinkWrap,
//           elevation: 0,
//           padding: getPadding,
//         ));
//       case AppButtonType.ghost:
//         return buttonStyle.merge(TextButton.styleFrom(
//           alignment: AlignmentDirectional.centerStart,
//           fixedSize: sizeButton,
//           elevation: 0,
//           padding: getPadding,
//           // foregroundColor: Theme.of(context).colorScheme.surface.withOpacity(0.4),
//         ));
//       case AppButtonType.light:
//         return buttonStyle.merge(ElevatedButton.styleFrom(
//           alignment: AlignmentDirectional.center,
//           fixedSize: sizeButton,
//           shadowColor: Colors.transparent,
//           elevation: 0,
//           padding: getPadding,
//           // foregroundColor: Theme.of(context).colorScheme.surface.withOpacity(0.4),
//         ));
//       case AppButtonType.dark:
//         return buttonStyle.merge(ElevatedButton.styleFrom(
//           alignment: AlignmentDirectional.center,
//           fixedSize: sizeButton,
//           elevation: 0,
//           padding: getPadding,
//           // foregroundColor: Theme.of(context).colorScheme.surface.withOpacity(0.4),
//         ));
//       case AppButtonType.gray:
//         return buttonStyle.merge(ElevatedButton.styleFrom(
//           alignment: AlignmentDirectional.center,
//           fixedSize: sizeButton,
//           elevation: 0,
//           padding: getPadding,
//           // foregroundColor: Theme.of(context).colorScheme.surface.withOpacity(0.4),
//         ));
//       case AppButtonType.oldPayment:
//       case AppButtonType.payment:
//         return buttonStyle.merge(ElevatedButton.styleFrom(
//           fixedSize: sizeButton,
//           elevation: 0,
//           backgroundColor:
//               _isOldPayment ? Colors.transparent : null,
//           padding:
//               _isOldPayment ? EdgeInsets.zero : getPadding,
//           // foregroundColor: Theme.of(context).colorScheme.surface.withOpacity(0.4),
//         ));
//     }
//   }

//   RoundedRectangleBorder? getShape(
//       Set<WidgetState> states) {
//     return getBorderRadius != null
//         ? RoundedRectangleBorder(
//             borderRadius: getBorderRadius!,
//             side: getSide(states) ?? BorderSide.none)
//         : null;
//   }

//   BorderRadiusGeometry? get getBorderRadius =>
//       widget.borderRadius ?? BorderRadius.circular(8.r);

//   EdgeInsetsGeometry get getPadding {
//     if (widget.padding != null) {
//       return widget.padding!;
//     }
//     if (_isLongerButton) {
//       return EdgeInsets.symmetric(
//           vertical: 12.h, horizontal: 16.w);
//     }

//     return EdgeInsets.symmetric(
//         vertical: 6.h, horizontal: 12.w);
//   }

//   Color? get getIconColor {
//     switch (widget._typeButton) {
//       case AppButtonType.secondary:
//       case AppButtonType.field:
//         return Colors.white;
//       //todo refactor colors
//       case AppButtonType.tertiary:
//       case AppButtonType.ghost:
//       case AppButtonType.light:
//       case AppButtonType.dark:
//       case AppButtonType.gray:
//       case AppButtonType.payment:
//       case AppButtonType.oldPayment:
//         return null;
//     }
//   }
// }

// MaterialColor get _brandPrimary => const MaterialColor(
//       0xffBC4105,
//       <int, Color>{
//         25: Color(0xffFFF3EE),
//         50: Color(0xffFEE6DA),
//         100: Color(0xffFECBB2),
//         200: Color(0xffFB5607),
//         300: Color(0xffE24D06),
//         400: Color(0xffC94506),
//         500: Color(0xffBC4105),
//         600: Color(0xff973404),
//         700: Color(0xff712703),
//         800: Color(0xff581E02),
//       },
//     );

// MaterialColor get _systemGray => const MaterialColor(
//       0xff667085,
//       <int, Color>{
//         25: Color(0xffFCFCFD),
//         50: Color(0xffF9FAFB),
//         100: Color(0xffF2F4F7),
//         200: Color(0xffEAECF0),
//         300: Color(0xffD0D5DD),
//         400: Color(0xff98A2B3),
//         500: Color(0xff667085),
//         600: Color(0xff475467),
//         700: Color(0xff344054),
//         800: Color(0xff1D2939),
//         900: Color(0xff101828),
//       },
//     );
