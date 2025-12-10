using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class UserBookmarkRepository : Repository<UserBookmark>, IUserBookmarkRepository
    {
        public UserBookmarkRepository(ApplicationDbContext context, ILogger<UserBookmarkRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<UserBookmark?> GetBookmarkAsync(Guid userId, Guid verseId)
        {
            try
            {
                return await _dbSet
                    .FirstOrDefaultAsync(b => b.UserId == userId && b.VerseId == verseId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmark: {UserId}, {VerseId}", userId, verseId);
                throw;
            }
        }

        public async Task<IEnumerable<UserBookmark>> GetBookmarksByUserAsync(Guid userId)
        {
            try
            {
                return await _dbSet
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Verse)
                    .ThenInclude(v => v!.Surah)
                    .OrderByDescending(b => b.CreateAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmarks by user: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<UserBookmark>> GetBookmarksByUserAndTypeAsync(Guid userId, BookmarkType type)
        {
            try
            {
                return await _dbSet
                    .Where(b => b.UserId == userId && b.Type == type)
                    .Include(b => b.Verse)
                    .ThenInclude(v => v!.Surah)
                    .OrderByDescending(b => b.CreateAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmarks by user and type: {UserId}, {Type}", userId, type);
                throw;
            }
        }

        public async Task<IEnumerable<UserBookmark>> GetBookmarksByVerseAsync(Guid verseId)
        {
            try
            {
                return await _dbSet
                    .Where(b => b.VerseId == verseId)
                    .Include(b => b.User)
                    .OrderByDescending(b => b.CreateAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmarks by verse: {VerseId}", verseId);
                throw;
            }
        }

        public async Task<UserBookmark?> GetBookmarkWithDetailsAsync(Guid bookmarkId)
        {
            try
            {
                return await _dbSet
                    .Include(b => b.User)
                    .Include(b => b.Verse)
                    .ThenInclude(v => v!.Surah)
                    .FirstOrDefaultAsync(b => b.Id == bookmarkId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmark with details: {BookmarkId}", bookmarkId);
                throw;
            }
        }

        public async Task<bool> BookmarkExistsAsync(Guid userId, Guid verseId)
        {
            try
            {
                return await _dbSet
                    .AnyAsync(b => b.UserId == userId && b.VerseId == verseId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking bookmark existence: {UserId}, {VerseId}", userId, verseId);
                throw;
            }
        }

        public async Task<Result<UserBookmark>> UpdateNoteAsync(Guid bookmarkId, string note)
        {
            try
            {
                var bookmark = await GetByIdAsync(bookmarkId);
                if (bookmark == null)
                    return Result<UserBookmark>.Failure($"Bookmark with ID {bookmarkId} not found");

                bookmark.Note = note;
                bookmark.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(bookmark);
                await _context.SaveChangesAsync();

                return Result<UserBookmark>.Success(bookmark);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating note for bookmark: {BookmarkId}", bookmarkId);
                return Result<UserBookmark>.Failure(ex.Message);
            }
        }

        public async Task<Result<UserBookmark>> ChangeBookmarkTypeAsync(Guid bookmarkId, BookmarkType newType)
        {
            try
            {
                var bookmark = await GetByIdAsync(bookmarkId);
                if (bookmark == null)
                    return Result<UserBookmark>.Failure($"Bookmark with ID {bookmarkId} not found");

                bookmark.Type = newType;
                bookmark.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(bookmark);
                await _context.SaveChangesAsync();

                return Result<UserBookmark>.Success(bookmark);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing bookmark type: {BookmarkId}", bookmarkId);
                return Result<UserBookmark>.Failure(ex.Message);
            }
        }

        public async Task<int> CountBookmarksByUserAsync(Guid userId)
        {
            try
            {
                return await _dbSet
                    .Where(b => b.UserId == userId)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting bookmarks by user: {UserId}", userId);
                throw;
            }
        }

        public async Task<Dictionary<BookmarkType, int>> GetBookmarkStatsByUserAsync(Guid userId)
        {
            try
            {
                var stats = await _dbSet
                    .Where(b => b.UserId == userId)
                    .GroupBy(b => b.Type)
                    .Select(g => new { Type = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(x => x.Type, x => x.Count);

                // Ensure all enum values are present
                foreach (BookmarkType type in Enum.GetValues(typeof(BookmarkType)))
                {
                    if (!stats.ContainsKey(type))
                    {
                        stats[type] = 0;
                    }
                }

                return stats;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookmark stats by user: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<UserBookmark>> GetRecentBookmarksByUserAsync(Guid userId, int count)
        {
            try
            {
                return await _dbSet
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Verse)
                    .ThenInclude(v => v!.Surah)
                    .OrderByDescending(b => b.CreateAt)
                    .Take(count)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recent bookmarks by user: {UserId}, {Count}", userId, count);
                throw;
            }
        }

        public async Task<Result<int>> RemoveAllBookmarksByUserAsync(Guid userId)
        {
            try
            {
                var bookmarks = await GetBookmarksByUserAsync(userId);

                if (!bookmarks.Any())
                    return Result<int>.Success(0);

                var result = await DeleteRangeAsync(bookmarks);
                return result.IsSuccess
                    ? Result<int>.Success(bookmarks.Count())
                    : Result<int>.Failure(result.Message!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing all bookmarks by user: {UserId}", userId);
                return Result<int>.Failure(ex.Message);
            }
        }

        public async Task<Result<int>> RemoveBookmarksByVerseAsync(Guid verseId)
        {
            try
            {
                var bookmarks = await GetBookmarksByVerseAsync(verseId);

                if (!bookmarks.Any())
                    return Result<int>.Success(0);

                var result = await DeleteRangeAsync(bookmarks);
                return result.IsSuccess
                    ? Result<int>.Success(bookmarks.Count())
                    : Result<int>.Failure(result.Message!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing bookmarks by verse: {VerseId}", verseId);
                return Result<int>.Failure(ex.Message);
            }
        }
    }
}
