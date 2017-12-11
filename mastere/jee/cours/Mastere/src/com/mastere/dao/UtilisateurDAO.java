package com.mastere.dao;

import java.util.List;

import org.hibernate.Session;

import com.mastere.db.HibernateUtil;
import com.mastere.metier.Groupe;
import com.mastere.metier.Utilisateur;

public class UtilisateurDAO extends UtilDAO<Utilisateur> {
	public UtilisateurDAO() {
		super(Utilisateur.class);
	}

	public  Utilisateur getByMailPassword(final String mail, final String password){
		//Session session = HibernateUtil.getSessionFactory().getCurrentSession();
		Utilisateur u = (Utilisateur) HibernateUtil.getSession()
			.createQuery("from Utilisateur where mail=? and password=?")
			.setParameter(0, mail)
			.setParameter(1, password)
			.getSingleResult();
		
/*		Utilisateur u = (Utilisateur) HibernateUtil.getSession()
				.createQuery("from Utilisateur where mail=:mail and password=:pass")
				.setParameter("mail", mail)
				.setParameter("pass", password)
				.getSingleResult();*/
		
		return u;
	}
	
	public List<Utilisateur> listAll(){
		return HibernateUtil.getSession().createQuery("from Utilisateur").list();
	}
	
	public  List<Groupe> getAllGroupe(){
		return HibernateUtil.getSession()
				.createQuery("from Groupe").list();
	}
	
	public  Groupe getGroupeById(long id){
		return HibernateUtil.getSession().load(Groupe.class, id);
		
	}
}
