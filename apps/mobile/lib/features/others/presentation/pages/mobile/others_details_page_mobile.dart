import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:mobile/features/others/presentation/state/bloc/others_cubit.dart';
import 'package:mobile/features/others/presentation/widgets/others_details_banner_widget.dart';
import 'package:mobile/features/others/presentation/widgets/others_details_item_widget.dart';

class OthersDetailsPageMobile extends StatelessWidget {
  final int categoryId;
  final String title;
  final dynamic icon;

  const OthersDetailsPageMobile({
    super.key,
    required this.categoryId,
    required this.title,
    required this.icon,
  });

  @override
  Widget build(BuildContext context) {
    return OthersDetailsView(title: title, icon: icon);
  }
}

class OthersDetailsView extends StatelessWidget {
  final String title;
  final dynamic icon;

  const OthersDetailsView({
    super.key,
    required this.title,
    required this.icon,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        AppAppBar(
          title: AppText.titleMedium(title),
          centerTitle: false,
        ),
        // Banner
        Padding(
          padding: const EdgeInsets.all(16.0),
          child: OthersDetailsBannerWidget(title: title, icon: icon),
        ),

        Expanded(
          child: BlocBuilder<OthersCubit, OthersState>(
            builder: (context, state) {
              if (state.status == const BlocStatus.loading()) {
                return const Center(child: CircularProgressIndicator());
              }

              return ListView.separated(
                padding: const EdgeInsets.symmetric(horizontal: 16),
                itemCount: state.items.length,
                separatorBuilder: (context, index) => 12.verticalSpace,
                itemBuilder: (context, index) {
                  final item = state.items[index];
                  return OthersDetailsItemWidget(item: item, index: index);
                },
              );
            },
          ),
        ),
      ],
    );
  }
}
