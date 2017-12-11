package com.mastere.dao;

import com.mastere.db.HibernateUtil;


public class UtilDAO<T> {
	Class<T> type;
	
	public UtilDAO(Class<T> type){
		this.type = type;
	}
	
	public void save(T o){
		
		HibernateUtil.getSession().saveOrUpdate(o);
	}
	
	@SuppressWarnings("unchecked")
	public  T getById(long id){
		return (T) HibernateUtil.getSession().load(type, id);
	}
	
	public void delete(T o){
		HibernateUtil.getSession().delete(o);
	}
	
}
