// 🐦 Flutter imports:
import 'package:core/core.dart';
import 'package:flutter/material.dart';

// 🌎 Project imports:

class AppText extends StatelessWidget {
  AppText(
    this.text, {
    super.key,
    this.strutStyle,
    this.textAlign,
    this.textDirection = TextDirection.ltr,
    this.locale,
    this.softWrap,
    this.overflow,
    this.textScaleFactor,
    this.maxLines,
    this.semanticsLabel,
    this.textWidthBasis,
    this.selectionColor,
    TextStyle? style,
    this.color,
    this.velocity,
    this.figmaLineHeight,
    this.scrollText = false,
  }) : style = (style ?? const TextStyle())
            .copyWith(color: color);

  final String text;
  final StrutStyle? strutStyle;
  final TextAlign? textAlign;
  final TextDirection? textDirection;
  final Locale? locale;
  final bool? softWrap;
  final TextOverflow? overflow;
  final double? textScaleFactor;
  final int? maxLines;
  final String? semanticsLabel;
  final TextWidthBasis? textWidthBasis;
  final Color? selectionColor;
  final TextStyle? style;
  final Color? color;
  final double? velocity;
  final bool scrollText;

  final double? figmaLineHeight;

  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      style: style?.copyWith(color: color),
      key: key,
      locale: locale,
      maxLines: maxLines,
      overflow: overflow,
      semanticsLabel: semanticsLabel,
      softWrap: softWrap,
      strutStyle: strutStyle,
      textAlign: textAlign,
      textDirection: textDirection,
      textScaleFactor: textScaleFactor,
    );
  }

  AppText.displayLarge(this.text,
      {super.key,
      this.figmaLineHeight,
      this.strutStyle,
      this.textAlign,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      this.scrollText = false,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.displayLarge
            ?.merge(style)
            .copyWith(
                fontFamily: fontFamily,
                fontWeight: fontWeight);

  AppText.displaySmall(this.text,
      {this.scrollText = false,
      this.strutStyle,
      this.textAlign,
      this.figmaLineHeight,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.displaySmall
            ?.merge(style)
            .copyWith(
                fontWeight: fontWeight,
                fontFamily: fontFamily);

  AppText.headlineLarge(this.text,
      {this.scrollText = false,
      this.strutStyle,
      this.textAlign,
      this.figmaLineHeight,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.headlineLarge
            ?.merge(style)
            .copyWith(
                fontWeight: fontWeight,
                fontFamily: fontFamily);

  AppText.headlineSmall(this.text,
      {this.scrollText = false,
      this.strutStyle,
      this.figmaLineHeight,
      this.textAlign,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.headlineSmall
            ?.merge(style)
            .copyWith(
                fontWeight: fontWeight,
                fontFamily: fontFamily);

  AppText.headlineMedium(this.text,
      {this.scrollText = false,
      this.strutStyle,
      this.textAlign,
      this.figmaLineHeight,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.headlineMedium
            ?.merge(style)
            .copyWith(
                fontWeight: fontWeight,
                fontFamily: fontFamily);

  AppText.titleLarge(this.text,
      {this.scrollText = false,
      this.figmaLineHeight,
      this.strutStyle,
      this.textAlign,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.titleLarge?.merge(style).copyWith(
            fontWeight: fontWeight, fontFamily: fontFamily);

  AppText.titleMedium(this.text,
      {this.scrollText = false,
      this.figmaLineHeight,
      this.strutStyle,
      this.textAlign,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.titleMedium
            ?.merge(style)
            .copyWith(
                fontWeight: fontWeight,
                fontFamily: fontFamily);

  AppText.titleSmall(this.text,
      {this.scrollText = false,
      this.strutStyle,
      this.figmaLineHeight,
      this.textAlign,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.titleSmall?.merge(style).copyWith(
            fontWeight: fontWeight, fontFamily: fontFamily);

  AppText.labelLarge(this.text,
      {this.scrollText = false,
      this.strutStyle,
      this.textAlign,
      this.textDirection,
      this.figmaLineHeight,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.labelLarge?.merge(style).copyWith(
            fontWeight: fontWeight, fontFamily: fontFamily);

  AppText.labelMedium(this.text,
      {this.scrollText = false,
      this.strutStyle,
      this.textAlign,
      this.textDirection,
      this.figmaLineHeight,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.labelMedium
            ?.merge(style)
            .copyWith(
                fontWeight: fontWeight,
                fontFamily: fontFamily);

  AppText.labelSmall(this.text,
      {this.scrollText = false,
      this.strutStyle,
      this.textAlign,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.figmaLineHeight,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.labelSmall?.merge(style).copyWith(
            fontWeight: fontWeight, fontFamily: fontFamily);

  AppText.bodyLarge(this.text,
      {this.scrollText = false,
      this.strutStyle,
      this.textAlign,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.figmaLineHeight,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.bodyLarge?.merge(style).copyWith(
            fontWeight: fontWeight, fontFamily: fontFamily);

  AppText.bodyMedium(this.text,
      {this.scrollText = false,
      this.strutStyle,
      this.textAlign,
      this.textDirection,
      this.locale,
      this.softWrap,
      this.overflow,
      this.textScaleFactor,
      this.figmaLineHeight,
      this.maxLines,
      this.semanticsLabel,
      this.textWidthBasis,
      this.selectionColor,
      this.color,
      this.velocity,
      super.key,
      TextStyle? style,
      FontWeight? fontWeight,
      String? fontFamily})
      : style = textTheme.bodyMedium?.merge(style).copyWith(
            fontWeight: fontWeight, fontFamily: fontFamily);
  AppText.bodySmall(
    this.text, {
    this.scrollText = false,
    this.strutStyle,
    this.textAlign,
    this.textDirection,
    this.locale,
    this.softWrap,
    this.overflow,
    this.textScaleFactor,
    this.maxLines,
    this.semanticsLabel,
    this.textWidthBasis,
    this.selectionColor,
    this.color,
    this.velocity,
    this.figmaLineHeight,
    TextStyle? style,
    FontWeight? fontWeight,
    String? fontFamily,
    super.key,
  }) : style = textTheme.bodySmall?.merge(style).copyWith(
            fontFamily: fontFamily, fontWeight: fontWeight);
}
