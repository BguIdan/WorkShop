using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using BL_Back_End;
using Database;

namespace DataBase
{
    public class Cache : ICache
    {
        private const string _Forum = "Forum";
        private const string _Subforum = "Subforum";
        private const string _Post = "Post";
        private const string _SuperUser = "SuperUser";
        private const string _User = "User";
        private const string _Moderator = "Moderator";
        private const string _Admin = "Admin";

        private TimeSpan _postExpiration = new TimeSpan(0, 1, 0);
        private DBClass db;
        public void setPostExpiration(int hours, int minutes, int secs)
        {
            _postExpiration = new TimeSpan(hours, minutes, secs);
        }

        private ObjectCache _cache;
        public Cache()
        {
            _cache = MemoryCache.Default;
        }


        public void AddSuperUser(String su)
        {
            if (su != null)
            {
                CacheItem user = new CacheItem(_SuperUser + su, su);
                _cache.Add(user, new CacheItemPolicy());
            }
        }
        public void AddForum(Forum f)
        {
            if (f != null)
            {
                CacheItem forum = new CacheItem(_Forum + f.forumName, f);
                _cache.Add(forum, new CacheItemPolicy());
            }
        }
        public void AddSubforum(SubForum sf)
        {
            if (sf != null)
            {
                CacheItem sforum = new CacheItem(_Subforum + sf.name + ";" + sf.forum, sf);
                _cache.Add(sforum, new CacheItemPolicy());
            }
        }
        public void AddUser(User user)
        {
            if (user != null)
            {
                CacheItem us = new CacheItem(_User + user.userName, user);
                _cache.Add(us, new CacheItemPolicy());
            }
        }
        public void AddPost(Post post)
        {
            if (post != null)
            {
                CacheItem pst = new CacheItem(_Post + post.id, post);
                _cache.Add(pst, new CacheItemPolicy());
            }
        }

        public void RemoveSuperUser(String su)
        {
            _cache.Remove(_SuperUser + su);
        }
        public void RemoveForum(String forumName)
        {
            _cache.Remove(_Forum + forumName);
        }
        public void RemoveSubforum(String sfname,String fname)
        {
            _cache.Remove(_Subforum + sfname + ";" + fname);
        }
        public void RemoveUser(String userName)
        {
            _cache.Remove(_User + userName);
        }
        public void RemovePost(int p)
        {
            _cache.Remove(_Post + p);
        }

        public Forum GetForum(string forumName)
        {
            object forum = _cache.Get(_Forum + forumName);
            if (forum != null)
                return (Forum)forum;
            forum = db.getforumByName(forumName);
            if (forum != null)
            {
                AddForum((Forum)forum);
                return (Forum)forum;
            }
            return null;
        }
        public SubForum GetSubforum(string subForumName,string forumName)
        {
            object subforum = _cache.Get(_Subforum + subForumName + ";" + forumName);
            if (subforum != null)
                return (SubForum)subforum;
            subforum = db.getSubForum(subForumName,forumName);
            if (subforum != null)
            {
                AddSubforum((SubForum)subforum);
                return (SubForum)subforum;
            }
            return null;
        }
        public User GetSuperUser(string su)
        {
            object superUser = _cache.Get(_SuperUser + su);
            if (superUser != null)
                return (User)_cache.Get(_User + su);
            superUser = db.getSuperUser(su);
            if (superUser != null)
            {
                AddSuperUser(su);
                if (_cache.Get(_User + su) == null)
                    AddUser((User)superUser);
                return (User)superUser;
            }
            return null;
        }
        public User GetUser(string name)
        {
            object user = _cache.Get(_User + name);
            if (user != null)
                return (User)user;
            user = db.getUser(name);
            if (user != null)
            {
                AddUser((User)user);
                return (User)user;
            }
            return null;
        }
        public Post GetPost(int p)
        {
            object post = _cache.Get(_Post + p.ToString());
            if (post != null)
                return (Post)post;
            AddPost(_dal.GetPost(p));
            return (Post)_cache.Get(_Post + p.ToString());
        }
    }
}
