﻿using Microsoft.EntityFrameworkCore;
using OnlineCourse.DAL.IRepositories;
using OnlineCourse.Domain.Commons;
using OnlineCourse.DAL.Contexts;
namespace OnlineCourse.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext appDbContex;
    public Repository()
    {
        this.appDbContex = new AppDbContext();
    }
    public async Task CreateAsync(T entity)
    {
        await this.appDbContex.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        this.appDbContex.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        this.appDbContex.Set<T>().Remove(entity);
    }

    public async Task<T> SelectById(long id)
       => await this.appDbContex.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));

    public IQueryable<T> SelectAll()
       => this.appDbContex.Set<T>().AsNoTracking().AsQueryable();
}
