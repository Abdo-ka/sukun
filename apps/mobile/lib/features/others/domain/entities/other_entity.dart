import 'package:equatable/equatable.dart';

class OtherCategoryEntity extends Equatable {
  final int id;
  final String title;
  final dynamic icon; // Can be String (SVG) or AssetGenImage (PNG)
  final bool isSvg;

  const OtherCategoryEntity({
    required this.id,
    required this.title,
    required this.icon,
    required this.isSvg,
  });

  @override
  List<Object?> get props => [id, title, icon, isSvg];
}

class OtherItemEntity extends Equatable {
  final int id;
  final String content;
  final String? title;

  const OtherItemEntity({
    required this.id,
    required this.content,
    this.title,
  });

  @override
  List<Object?> get props => [id, content, title];
}
