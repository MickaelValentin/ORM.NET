package com.mastere.db;

import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.boot.Metadata;
import org.hibernate.boot.MetadataSources;
import org.hibernate.boot.registry.StandardServiceRegistry;
import org.hibernate.boot.registry.StandardServiceRegistryBuilder;
import org.hibernate.cfg.Configuration;

public class HibernateUtil {
	
	private static final SessionFactory sessionFact;
	
	static{
		final StandardServiceRegistry registry = 
				new StandardServiceRegistryBuilder()
				.configure("hibernate.cfg.xml")
				.build();
		try{
			Metadata meta = new MetadataSources(registry)
			.getMetadataBuilder().build();
			
			sessionFact =meta.getSessionFactoryBuilder().build(); 
		}catch(Throwable e){
			System.err.println(e);
			throw new ExceptionInInitializerError(e);
		}
	}
	
	public static SessionFactory getSessionFactory(){
		return sessionFact;
	}
	
	public static final Session getSession(){
		Session sess = getSessionFactory().getCurrentSession();
		if(! sess.getTransaction().isActive())
			sess.beginTransaction();
		return sess;
	}
	
	
}
