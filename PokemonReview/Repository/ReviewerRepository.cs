﻿using Microsoft.EntityFrameworkCore;
using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;
using System.Diagnostics.Metrics;

namespace PokemonReview.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;
        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(rw => rw.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(rw => rw.Id == reviewerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }
    }
}
