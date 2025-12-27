import 'package:auto_route/auto_route.dart';
import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:mobile/features/others/presentation/pages/mobile/others_details_page_mobile.dart';

@RoutePage()
class OthersDetailsPage extends StatelessWidget {
  final int categoryId;
  final String title;
  final dynamic icon;

  const OthersDetailsPage({
    super.key,
    required this.categoryId,
    required this.title,
    required this.icon,
  });

  @override
  Widget build(BuildContext context) {
    //TODO:(Aya) I see here now using AppScaffold but suggest to move it to OthersDetailsPageMobile screen instead of here
    //! TODO:(Aya) try using mason make feautre to generate automatically all files you need (not important but easier)
    return AppScaffold(
      body: PageLayoutBuilder(
        mobile: (context) => OthersDetailsPageMobile(
          categoryId: categoryId,
          title: title,
          icon: icon,
        ),
      ),
    );
  }
}
