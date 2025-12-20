using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.BookMark.Request;
using Sukun.Application.Dtos.BookMark.Responce;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Domin.Helping;
using Sukun.Infrastructure.InfrastructureBases;
using System.Linq;

namespace Sukun.Application.Implemantation
{
    public class UserBookmarkService : BaseService, IUserBookmarkService
    {
        public UserBookmarkService(IUnitOfWork unitOfWork, ILogger<UserBookmarkService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<Result<UserBookmarkResponseDto>> GetByIdAsync(Guid bookmarkId)
        {
            try
            {
                _logger.LogDebug("Getting bookmark by ID: {BookmarkId}", bookmarkId);
                var bookmark = await _unitOfWork.Repository<UserBookmark>().GetByIdAsync(bookmarkId);
                if (bookmark == null)
                    return Result<UserBookmarkResponseDto>.NotFound($"Bookmark with ID {bookmarkId} not found");

                return Result<UserBookmarkResponseDto>.Success(UserBookmarkMapper.ToResponseDto(bookmark));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmark by ID: {BookmarkId}", bookmarkId);
                return Result<UserBookmarkResponseDto>.Failure($"Error retrieving bookmark: {ex.Message}");
            }
        }

        public async Task<Result<UserBookmarkResponseDto>> GetBookmarkAsync(Guid userId, Guid verseId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<UserBookmarkResponseDto>.NotFound($"Not found User by id : {userId}");
                var verse = await _unitOfWork.Quran.GetVerseByIdAsync(verseId);
                if (verse == null)
                    return Result<UserBookmarkResponseDto>.NotFound($"Not found Verse by id : {userId}");
                _logger.LogDebug("Getting bookmark for user {UserId} and verse {VerseId}", userId, verseId);
                var bookmark = await _unitOfWork.UserBookmarks.GetBookmarkAsync(userId, verseId);
                if (bookmark == null)
                    return Result<UserBookmarkResponseDto>.NotFound($"Bookmark not found for user {userId} and verse {verseId}");

                return Result<UserBookmarkResponseDto>.Success(UserBookmarkMapper.ToResponseDto(bookmark));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmark");
                return Result<UserBookmarkResponseDto>.Failure($"Error retrieving bookmark: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserBookmarkResponseDto>>> GetBookmarksByUserAsync(Guid userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<IEnumerable<UserBookmarkResponseDto>>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Getting bookmarks by user: {UserId}", userId);
                var bookmarks = await _unitOfWork.UserBookmarks.GetBookmarksByUserAsync(userId);
                return Result<IEnumerable<UserBookmarkResponseDto>>.Success(bookmarks.Select(UserBookmarkMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmarks by user: {UserId}", userId);
                return Result<IEnumerable<UserBookmarkResponseDto>>.Failure($"Error retrieving bookmarks: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserBookmarkResponseDto>>> GetBookmarksByUserAndTypeAsync(Guid userId, BookmarkType type)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<IEnumerable<UserBookmarkResponseDto>>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Getting bookmarks by user {UserId} and type {Type}", userId, type);
                var bookmarks = await _unitOfWork.UserBookmarks.GetBookmarksByUserAndTypeAsync(userId, type);
                return Result<IEnumerable<UserBookmarkResponseDto>>.Success(bookmarks.Select(UserBookmarkMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmarks by user and type");
                return Result<IEnumerable<UserBookmarkResponseDto>>.Failure($"Error retrieving bookmarks: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserBookmarkResponseDto>>> GetBookmarksByVerseAsync(Guid verseId)
        {
            try
            {
                _logger.LogDebug("Getting bookmarks by verse: {VerseId}", verseId);
                var bookmarks = await _unitOfWork.UserBookmarks.GetBookmarksByVerseAsync(verseId);
                return Result<IEnumerable<UserBookmarkResponseDto>>.Success(bookmarks.Select(UserBookmarkMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmarks by verse: {VerseId}", verseId);
                return Result<IEnumerable<UserBookmarkResponseDto>>.Failure($"Error retrieving bookmarks: {ex.Message}");
            }
        }

        public async Task<Result<UserBookmarkResponseDto>> GetBookmarkWithDetailsAsync(Guid bookmarkId)
        {
            try
            {
                _logger.LogDebug("Getting bookmark with details by ID: {BookmarkId}", bookmarkId);
                var bookmark = await _unitOfWork.UserBookmarks.GetBookmarkWithDetailsAsync(bookmarkId);
                if (bookmark == null)
                    return Result<UserBookmarkResponseDto>.NotFound($"Bookmark with ID {bookmarkId} not found");

                return Result<UserBookmarkResponseDto>.Success(UserBookmarkMapper.ToResponseDto(bookmark));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmark with details: {BookmarkId}", bookmarkId);
                return Result<UserBookmarkResponseDto>.Failure($"Error retrieving bookmark: {ex.Message}");
            }
        }

        public async Task<Result<bool>> BookmarkExistsAsync(Guid userId, Guid verseId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<bool>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Checking bookmark existence: {UserId}, {VerseId}", userId, verseId);
                var exists = await _unitOfWork.UserBookmarks.BookmarkExistsAsync(userId, verseId);
                return Result<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking bookmark existence");
                return Result<bool>.Failure($"Error checking bookmark: {ex.Message}");
            }
        }

        public async Task<Result<UserBookmarkResponseDto>> UpdateNoteAsync(Guid bookmarkId, string note)
        {
            try
            {
                _logger.LogDebug("Updating note for bookmark: {BookmarkId}", bookmarkId);
                var result = await _unitOfWork.UserBookmarks.UpdateNoteAsync(bookmarkId, note);
                if (!result.IsSuccess)
                    return Result<UserBookmarkResponseDto>.NotFound(result.Message);

                await _unitOfWork.CompleteAsync();
                return Result<UserBookmarkResponseDto>.Success(UserBookmarkMapper.ToResponseDto(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating note for bookmark: {BookmarkId}", bookmarkId);
                return Result<UserBookmarkResponseDto>.Failure($"Error updating bookmark: {ex.Message}");
            }
        }

        public async Task<Result<UserBookmarkResponseDto>> ChangeBookmarkTypeAsync(Guid bookmarkId, BookmarkType newType)
        {
            try
            {
               
                _logger.LogDebug("Changing bookmark type: {BookmarkId} to {NewType}", bookmarkId, newType);
                var result = await _unitOfWork.UserBookmarks.ChangeBookmarkTypeAsync(bookmarkId, newType);
                if (!result.IsSuccess)
                    return Result<UserBookmarkResponseDto>.NotFound(result.Message);

                await _unitOfWork.CompleteAsync();
                return Result<UserBookmarkResponseDto>.Success(UserBookmarkMapper.ToResponseDto(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing bookmark type: {BookmarkId}", bookmarkId);
                return Result<UserBookmarkResponseDto>.Failure($"Error updating bookmark: {ex.Message}");
            }
        }

        public async Task<Result<int>> CountBookmarksByUserAsync(Guid userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<int>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Counting bookmarks by user: {UserId}", userId);
                var count = await _unitOfWork.UserBookmarks.CountBookmarksByUserAsync(userId);
                return Result<int>.Success(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting bookmarks by user: {UserId}", userId);
                return Result<int>.Failure($"Error counting bookmarks: {ex.Message}");
            }
        }

        public async Task<Result<Dictionary<BookmarkType, int>>> GetBookmarkStatsByUserAsync(Guid userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<Dictionary<BookmarkType, int>>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Getting bookmark stats by user: {UserId}", userId);
                var stats = await _unitOfWork.UserBookmarks.GetBookmarkStatsByUserAsync(userId);
                return Result<Dictionary<BookmarkType, int>>.Success(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmark stats by user: {UserId}", userId);
                return Result<Dictionary<BookmarkType, int>>.Failure($"Error retrieving stats: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserBookmarkResponseDto>>> GetRecentBookmarksByUserAsync(Guid userId, int count = 10)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<IEnumerable<UserBookmarkResponseDto>>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Getting recent bookmarks by user: {UserId}, count: {Count}", userId, count);
                var bookmarks = await _unitOfWork.UserBookmarks.GetRecentBookmarksByUserAsync(userId, count);
                return Result<IEnumerable<UserBookmarkResponseDto>>.Success(bookmarks.Select(UserBookmarkMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recent bookmarks by user");
                return Result<IEnumerable<UserBookmarkResponseDto>>.Failure($"Error retrieving bookmarks: {ex.Message}");
            }
        }

        public async Task<Result<int>> RemoveAllBookmarksByUserAsync(Guid userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<int>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Removing all bookmarks by user: {UserId}", userId);
                var result = await _unitOfWork.UserBookmarks.RemoveAllBookmarksByUserAsync(userId);
                await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing all bookmarks by user: {UserId}", userId);
                return Result<int>.Failure($"Error removing bookmarks: {ex.Message}");
            }
        }

        public async Task<Result<int>> RemoveBookmarksByVerseAsync(Guid verseId)
        {
            try
            {

                var verse = await _unitOfWork.Quran.GetVerseByIdAsync(verseId);
                if (verse == null)
                    return Result<int>.NotFound($"Not found Verse by id : {verseId}");
                _logger.LogDebug("Removing bookmarks by verse: {VerseId}", verseId);
                var result = await _unitOfWork.UserBookmarks.RemoveBookmarksByVerseAsync(verseId);
                await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing bookmarks by verse: {VerseId}", verseId);
                return Result<int>.Failure($"Error removing bookmarks: {ex.Message}");
            }
        }
        public async Task<Result<UserBookmarkResponseDto>> CreateBookmarkAsync(Guid userId, UserBookmarkCreateDto dto)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<UserBookmarkResponseDto>.NotFound($"Not found User by id : {userId}");
                _logger.LogInformation("Creating bookmark for user: {UserId}, verse: {VerseId}", userId, dto.VerseId);
                var existingBookmark = await _unitOfWork.UserBookmarks.GetBookmarkAsync(userId, dto.VerseId);
                if (existingBookmark != null)
                    return Result<UserBookmarkResponseDto>.Failure("Bookmark already exists for this verse");

                var verse = await _unitOfWork.Quran.GetVerseByIdAsync(dto.VerseId);
                if (verse == null)
                    return Result<UserBookmarkResponseDto>.NotFound($"Verse with ID {dto.VerseId} not found");

                var bookmark = UserBookmarkMapper.FromCreateDto(dto, userId);
                await _unitOfWork.UserBookmarks.AddAsync(bookmark);
                await _unitOfWork.CompleteAsync();

                bookmark.Verse = verse;
                _logger.LogInformation("Bookmark created successfully with ID: {BookmarkId}", bookmark.Id);
                return Result<UserBookmarkResponseDto>.Success(UserBookmarkMapper.ToResponseDto(bookmark));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating bookmark for user: {UserId}, verse: {VerseId}", userId, dto.VerseId);
                return Result<UserBookmarkResponseDto>.Failure($"Error creating bookmark: {ex.Message}");
            }
        }

        public async Task<Result<PagedResponseDto<UserBookmarkResponseDto>>> GetBookmarksByUserPagedAsync(Guid userId, PagedRequestDto request)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<PagedResponseDto<UserBookmarkResponseDto>>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Getting bookmarks for user: {UserId}, page: {PageNumber}", userId, request.PageNumber);
                var query =  _unitOfWork.UserBookmarks.AsQueryable();
                query.Where(b=>b.UserId == userId);
                if (request.SearchTerm != null)
                {
                   query= query.Where(b =>b.Verse.Surah.Name == request.SearchTerm || b.Verse.Text == request.SearchTerm);
                }
                var totalCount = query.Count();
                query = query.OrderBy(q => q.CreateAt);
                var pagedBookmarks = await query.ApplyPaginatedAsync(request.PageNumber,
                    request.PageSize).Include(x => x.Verse).ThenInclude(v => v.Surah).ToListAsync();

                var pagedBookmarksDto = pagedBookmarks.Select(UserBookmarkMapper.ToResponseDto).ToList();
                var response = CreatePagedResponse(pagedBookmarksDto, request.PageNumber, request.PageSize, totalCount);
                return Result<PagedResponseDto<UserBookmarkResponseDto>>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmarks for user: {UserId}", userId);
                return Result<PagedResponseDto<UserBookmarkResponseDto>>.Failure($"Error getting bookmarks: {ex.Message}");
            }
        }

        public async Task<Result<UserBookmarkResponseDto>> UpdateBookmarkAsync(Guid bookmarkId, UserBookmarkUpdateDto dto)
        {
            try
            {
                _logger.LogInformation("Updating bookmark with ID: {BookmarkId}", bookmarkId);
                var bookmark = await _unitOfWork.UserBookmarks.GetByIdAsync(bookmarkId);
                if (bookmark == null)
                    return Result<UserBookmarkResponseDto>.NotFound($"Bookmark with ID {bookmarkId} not found");

                UserBookmarkMapper.UpdateFromDto(bookmark, dto);
                await _unitOfWork.UserBookmarks.UpdateAsync(bookmark);
                await _unitOfWork.CompleteAsync();

                var updatedBookmark = await _unitOfWork.UserBookmarks.GetBookmarkWithDetailsAsync(bookmarkId);
                _logger.LogInformation("Bookmark updated successfully with ID: {BookmarkId}", bookmarkId);
                return Result<UserBookmarkResponseDto>.Success(UserBookmarkMapper.ToResponseDto(updatedBookmark!));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating bookmark with ID: {BookmarkId}", bookmarkId);
                return Result<UserBookmarkResponseDto>.Failure($"Error updating bookmark: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteBookmarkAsync(Guid bookmarkId)
        {
            try
            {
                _logger.LogInformation("Deleting bookmark with ID: {BookmarkId}", bookmarkId);
                var bookmark = await _unitOfWork.UserBookmarks.GetByIdAsync(bookmarkId);
                if (bookmark == null)
                    return Result<bool>.NotFound($"Bookmark with ID {bookmarkId} not found");

                await _unitOfWork.UserBookmarks.DeleteAsync(bookmark);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Bookmark deleted successfully with ID: {BookmarkId}", bookmarkId);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting bookmark with ID: {BookmarkId}", bookmarkId);
                return Result<bool>.Failure($"Error deleting bookmark: {ex.Message}");
            }
        }

        public async Task<Result<UserBookmarkResponseDto>> UpdateBookmarkNoteAsync(Guid bookmarkId, string note)
        {
            try
            {
                _logger.LogInformation("Updating note for bookmark: {BookmarkId}", bookmarkId);
                var result = await _unitOfWork.UserBookmarks.UpdateNoteAsync(bookmarkId, note);
                if (!result.IsSuccess)
                    return Result<UserBookmarkResponseDto>.Failure(result.Message!);

                var bookmark = await _unitOfWork.UserBookmarks.GetBookmarkWithDetailsAsync(bookmarkId);
                return Result<UserBookmarkResponseDto>.Success(UserBookmarkMapper.ToResponseDto(bookmark!));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating note for bookmark: {BookmarkId}", bookmarkId);
                return Result<UserBookmarkResponseDto>.Failure($"Error updating bookmark note: {ex.Message}");
            }
        }
    }
}