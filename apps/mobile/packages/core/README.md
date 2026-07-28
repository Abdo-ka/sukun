# Core Package

A highly optimized Flutter package providing reusable widgets, utilities, and architectural patterns following Clean Architecture principles.

## Features

### 🎨 **UI Components**
- **AppButton**: Customizable button with multiple variants (light, dark, tertiary, etc.)
- **AppText**: Optimized text widget with automatic text scaling
- **AppImage**: Smart image widget with caching for both SVG and raster images
- **AppTextField**: Feature-rich text input with validation
- **AppCheckbox**: Animated checkbox with custom styling
- **AppScaffold**: Optimized scaffold wrapper with consistent defaults
- **AppPasswordField**: Secure password input with visibility toggle

### 🔄 **State Management**
- **CommonState**: Type-safe state management with sealed classes
- **ResultBuilder**: BlocBuilder wrapper for handling loading/success/error states
- **BlocStatus**: Status tracking for bloc states

### 🌐 **Network Layer**
- **DioClient**: Optimized HTTP client with:
  - Connection pooling (5 connections per host)
  - Configurable timeouts (10s connect, 30s receive/send)
  - Automatic error mapping
  - Custom exception handling

### 📦 **Utilities**
- **SecureFilePicker**: Image picker with:
  - File validation and size limits
  - Smart compression with quality steps
  - Caching to avoid redundant operations
  - Support for cropping
- **CoreHelperFunctions**: Date/time formatting, JSON conversion, pagination
- **PaginationModel**: Type-safe pagination with helper methods

### 🎯 **Architecture Patterns**
- **UseCase**: Clean Architecture use case pattern
- **FutureResult**: Either monad for functional error handling
- **AppException**: Structured exception hierarchy

## Performance Optimizations

### ✅ **Widget Optimizations**
- Const constructors throughout for better widget rebuild performance
- Inline pragma annotations for frequently called methods
- Cached color computations
- Smart use of `RepaintBoundary` where needed

### ✅ **Image Optimizations**
- Cache width/height hints for efficient memory usage
- Smart SVG detection (case-insensitive)
- Network image caching
- Compression with configurable quality steps
- Source file caching to avoid redundant operations

### ✅ **Network Optimizations**
- HTTP connection pooling
- Configurable timeouts
- Efficient error handling
- Request/response interceptors

### ✅ **State Management**
- Efficient state checking with inline methods
- Sealed classes for exhaustive pattern matching
- Type-safe state transitions

### ✅ **Date/Time**
- Cached DateFormat instances
- Reusable formatters for better performance

## Usage

```dart
import 'package:core/core.dart';

// Using AppButton
AppButton.light(
  title: 'Click Me',
  onPressed: () {},
)

// Using AppImage with caching
AppImage.network(
  'https://example.com/image.png',
  width: 200,
  height: 200,
)

// Using ResultBuilder
ResultBuilder<MyBloc, MyData>(
  loaded: (data) => Text(data.toString()),
  loading: () => CircularProgressIndicator(),
  error: (error) => Text(error.message),
  empty: () => Text('No data'),
  initial: () => Text('Start'),
)
```

## Requirements

- Dart SDK: `>=3.5.0 <4.0.0`
- Flutter: `>=3.24.0`

## Dependencies

See `pubspec.yaml` for the full list of dependencies. All dependencies are kept up-to-date with their latest stable versions.

## Contributing

This package follows Flutter best practices and Clean Architecture principles. When contributing:

1. Maintain const constructors where possible
2. Add documentation to public APIs
3. Follow the existing code style
4. Write tests for new features
5. Update this README with new features

## License

See LICENSE file for details.
