package com.mastere.filtres;

import java.io.IOException;

import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;

import com.mastere.db.HibernateUtil;

public class FiltreHibernate implements Filter {

	@Override
	public void destroy() {
		// TODO Auto-generated method stub

	}

	@Override
	public void doFilter(ServletRequest arg0, ServletResponse arg1, FilterChain arg2)
			throws IOException, ServletException {

		arg2.doFilter(arg0, arg1);
		try {
			if (HibernateUtil.getSession().getTransaction().isActive())
				HibernateUtil.getSession().getTransaction().commit();
		} catch (Exception e) {
			HibernateUtil.getSession().getTransaction().rollback();
		} finally {
			if (HibernateUtil.getSession().isOpen())
				HibernateUtil.getSession().close();
		}
	}

	@Override
	public void init(FilterConfig arg0) throws ServletException {
		// TODO Auto-generated method stub

	}

}
