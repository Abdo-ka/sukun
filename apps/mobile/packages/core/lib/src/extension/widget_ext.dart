import 'package:flutter/material.dart';

extension PaddingApp on Widget {
  Widget wrapPadding([EdgeInsetsGeometry value = const EdgeInsets.all(16)]) {
    return Padding(
      padding: value,
      child: this,
    );
  }

  Widget scrollable({Axis scrollDirection = Axis.vertical}) =>
      SingleChildScrollView(scrollDirection: scrollDirection, child: this);

  Widget paddingAll(double value) =>
      Padding(padding: EdgeInsets.all(value), child: this);

  Widget positioned({
    double? top,
    double? bottom,
    double? left,
    double? right,
  }) =>
      Positioned(
        top: top,
        bottom: bottom,
        left: left,
        right: right,
        child: this,
      );

  Widget positionedDirectional({
    double? top,
    double? bottom,
    double? start,
    double? end,
  }) =>
      PositionedDirectional(
        top: top,
        bottom: bottom,
        start: start,
        end: end,
        child: this,
      );

  Widget center() => Center(child: this);

  Widget row(
          [MainAxisAlignment mainAxisAlignment = MainAxisAlignment.center]) =>
      Row(
        mainAxisAlignment: mainAxisAlignment,
        children: [this],
      );
  Widget column() => Column(
        children: [this],
      );

  Widget expand({int flex = 1}) => Expanded(flex: flex, child: this);
}
