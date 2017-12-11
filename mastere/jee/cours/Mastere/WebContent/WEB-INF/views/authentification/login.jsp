<div class="row">
	<div class="col-md-8 col-md-offset-2">
		<form method="post" action="<c:url value='/login'/>">
			<div class="form-group">
				<label for="exampleInputEmail1">Email</label> <input name="mail"
					type="email" class="form-control" id="exampleInputEmail1"
					placeholder="Email">
			</div>
			<div class="form-group">
				<label for="exampleInputPassword1">Mot de passe</label> <input
					name="password" type="password" class="form-control"
					id="exampleInputPassword1" placeholder="Password">
			</div>

			<button type="submit" class="btn btn-default">Login</button>
		</form>
	</div>
</div>