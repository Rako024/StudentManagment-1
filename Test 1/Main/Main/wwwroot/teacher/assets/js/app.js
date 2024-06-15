/*
Author       : Dreamstechnologies
Template Name: SmartHR - Bootstrap Admin Template
Version      : 4.0
*/

$(document).ready(function() {
	
	// Variables declarations
	
	var $wrapper = $('.main-wrapper');
	var $pageWrapper = $('.page-wrapper');
	var $slimScrolls = $('.slimscroll');

	// Loader
	
	setTimeout(function () {
		$('#loader-wrapper');
		setTimeout(function () {
			$("#loader-wrapper").hide();
		}, 100);
	}, 500);
	
	// Sidebar
	
	var Sidemenu = function() {
		this.$menuItem = $('#sidebar-menu a');
	};
	function init() {
		var $this = Sidemenu;
		$('#sidebar-menu a').on('click', function(e) {
			if($(this).parent().hasClass('submenu')) {
				e.preventDefault();
			}
			if(!$(this).hasClass('subdrop')) {
				$('ul', $(this).parents('ul:first')).hide(350);
				$('a', $(this).parents('ul:first')).removeClass('subdrop');
				$(this).next('ul').show(350);
				$(this).addClass('subdrop');
			} else if($(this).hasClass('subdrop')) {
				$(this).removeClass('subdrop');
				$(this).next('ul').hide(350);
			}
		});
		$('#sidebar-menu ul li.submenu a.active').parents('li:last').children('a:first').addClass('active').trigger('click');

		
	}
	
	// Sidebar Initiate
	init();

	$(document).on('click', '.select-people-checkbox', function() {
		$(this).toggleClass('checkbox-checked');
		return false;
	});

	$(document).on('click', '.star-select i', function() {
		$(this).toggleClass('filled');
		return false;
	});

	$(function() {
		$("#table-filter").on("click", function(a) {
		  $(".form-sorts").toggleClass("table-filter-show");
		  a.stopPropagation()
		});
		$(document).on("click", function(a) {
		  if ($(a.target).is(".form-sorts") === false) {
			$(".form-sorts").removeClass("table-filter-show");
		  }
		});
	  });
	  $('.filter-dropdown-menu').click(function(event){
		event.stopPropagation();
	  });

	// Password toggle
	
	$("#toggle-password").click(function () {
		$( this ).toggleClass("fa-eye fa-eye-slash");
        if ($("#password").attr("type") == "password")
        {
            $("#password").attr("type", "text");
        } else
        {
            $("#password").attr("type", "password");
        }
    });

	if($('.summernote').length > 0) {
		//var editorheight = $('.editor-card').height()-100;
        $('.summernote').summernote({
			placeholder: 'Description',
		    focus: true,
			minHeight: 100,
			disableResizeEditor: false,
			toolbar: [
				['fullscreen',],
				['fontname', ['fontname']],
				['undo'],
				['redo'],
				['datetimepicker'],
				['fontsize', ['fontsize']],
				['font', ['bold', 'italic', 'underline', 'clear']],
				['color', ['color']],
				['para', ['ul', 'ol', 'paragraph']],
				['insert', ['link', 'picture']]
			  ],
			// set focus to editable area after initializing summernote
		});
    }

	// Summernote
	
	if($('#summernote').length > 0) {
        $('#summernote').summernote({
		  height: 300,                 // set editor height
		  minHeight: null,             // set minimum height of editor
		  maxHeight: null,             // set maximum height of editor
		  focus: true                  // set focus to editable area after initializing summernote
		});
    }
	// editor
	if ($('#editor').length > 0) {
		ClassicEditor
		.create( document.querySelector( '#editor' ), {
			toolbar: {
                items: [
                    'heading', '|',
                    'fontfamily', 'fontsize', '|',
                    'alignment', '|', 
                    'fontColor', 'fontBackgroundColor', '|',
                    'bold', 'italic', 'strikethrough', 'underline', 'subscript', 'superscript', '|',
                    'link', '|',
                    'outdent', 'indent', '|',
                    'bulletedList', 'numberedList', 'todoList', '|',
                    'code', 'codeBlock', '|',
                    'insertTable', '|',
                    'uploadImage', 'blockQuote', '|',
                    'undo', 'redo'
                ],
                shouldNotGroupWhenFull: true
            }
		} )
		.then( editor => {
			window.editor = editor;
		} )
		.catch( err => {
			console.error( err.stack );
		} );
	}

	// Counter 
	
	if($('.counter').length > 0) {
		$('.counter').counterUp({
			 delay: 20,
			 time: 2000
		});
	 }
	 
	 if($('#timer-countdown').length > 0) {
		 $( '#timer-countdown' ).countdown( {
			 from: 180, // 3 minutes (3*60)
			 to: 0, // stop at zero
			 movingUnit: 1000, // 1000 for 1 second increment/decrements
			 timerEnd: undefined,
			 outputPattern: '$day Day $hour : $minute : $second',
			 autostart: true
		 });
	 }
	 
	 if($('#timer-countup').length > 0) {
		 $( '#timer-countup' ).countdown( {
			 from: 0,
			 to: 180 
		 });
	 }
	 
	 if($('#timer-countinbetween').length > 0) {
		 $( '#timer-countinbetween' ).countdown( {
			 from: 30,
			 to: 20 
		 });
	 }
	 
	 if($('#timer-countercallback').length > 0) {
		 $( '#timer-countercallback' ).countdown( {
			 from: 10,
			 to: 0,
			 timerEnd: function() {
				 this.css( { 'text-decoration':'line-through' } ).animate( { 'opacity':.5 }, 500 );
			 } 
		 });
	 }
	 
	 if($('#timer-outputpattern').length > 0) {
		 $( '#timer-outputpattern' ).countdown( {
			 outputPattern: '$day Days $hour Hour $minute Min $second Sec..',
			 from: 60 * 60 * 24 * 3
		 });
	 }

	// Mobile menu sidebar overlay
	
	$('body').append('<div class="sidebar-overlay"></div>');
	$(document).on('click', '#mobile_btn', function() {
		$wrapper.toggleClass('slide-nav');
		$('.sidebar-overlay').toggleClass('opened');
		$('html').addClass('menu-opened');
		$('#task_window').removeClass('opened');
		return false;
	});
	
	$(".sidebar-overlay").on("click", function () {
			$('html').removeClass('menu-opened');
			$(this).removeClass('opened');
			$wrapper.removeClass('slide-nav');
			$('.sidebar-overlay').removeClass('opened');
			$('#task_window').removeClass('opened');
	});
	
	// Chat sidebar overlay
	
	$(document).on('click', '#task_chat', function() {
		$('.sidebar-overlay').toggleClass('opened');
		$('#task_window').addClass('opened');
		return false;
	});
	
	// Select 2
	
	if($('.select').length > 0) {
		$('.select').select2({
			minimumResultsForSearch: -1,
			width: '100%'
		});
	}

	if($('.custom-file-container').length > 0) {
        //First upload
        var firstUpload = new FileUploadWithPreview('myFirstImage')
        //Second upload
        var secondUpload = new FileUploadWithPreview('mySecondImage')
	}

	// Clipboard 
	
	if($('.clipboard').length > 0) {
		var clipboard = new Clipboard('.btn');
	}
	// Popover
	
	if($('.popover-list').length > 0) {
		var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
		var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
		  return new bootstrap.Popover(popoverTriggerEl)
		})
	}
	// Modal Popup hide show

	if($('.modal').length > 0 ){
		var modalUniqueClass = ".modal";
		$('.modal').on('show.bs.modal', function(e) {
		  var $element = $(this);
		  var $uniques = $(modalUniqueClass + ':visible').not($(this));
		  if ($uniques.length) {
			$uniques.modal('hide');
			$uniques.one('hidden.bs.modal', function(e) {
			  $element.modal('show');
			});
			return false;
		  }
		});
	}
	
	// Floating Label

	if($('.floating').length > 0 ){
		$('.floating').on('focus blur', function (e) {
		$(this).parents('.form-focus').toggleClass('focused', (e.type === 'focus' || this.value.length > 0));
		}).trigger('blur');
	}

	// Date Range Picker
	if($('.bookingrange').length > 0) {
		var start = moment().subtract(6, 'days');
		var end = moment();

		function booking_range(start, end) {
			$('.bookingrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
		}

		$('.bookingrange').daterangepicker({
			startDate: start,
			endDate: end,
			ranges: {
				'Today': [moment(), moment()],
				'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
				'Last 7 Days': [moment().subtract(6, 'days'), moment()],
				'Last 30 Days': [moment().subtract(29, 'days'), moment()],
				'This Month': [moment().startOf('month'), moment().endOf('month')],
				'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
			}
		}, booking_range);

		booking_range(start, end);
	}
	
	// Sidebar Slimscroll

	if($slimScrolls.length > 0) {
		$slimScrolls.slimScroll({
			height: 'auto',
			width: '100%',
			position: 'right',
			size: '7px',
			color: '#ccc',
			wheelStep: 10,
			touchScrollStep: 100
		});
		var wHeight = $(window).height() - 60;
		$slimScrolls.height(wHeight);
		$('.sidebar .slimScrollDiv').height(wHeight);
		$(window).resize(function() {
			var rHeight = $(window).height() - 60;
			$slimScrolls.height(rHeight);
			$('.sidebar .slimScrollDiv').height(rHeight);
		});
	}
	
	// Page Content Height

	var pHeight = $(window).height();
	$pageWrapper.css('min-height', pHeight);
	$(window).resize(function() {
		var prHeight = $(window).height();
		$pageWrapper.css('min-height', prHeight);
	});
	
	// Date Time Picker
	
	if($('.datetimepicker').length > 0) {
		$('.datetimepicker').datetimepicker({
			//format: 'DD/MM/YYYY',
			format: 'DD-MM-YYYY',
			icons: {
				up: "fa fa-angle-up",
				down: "fa-solid fa-angle-down",
				next: 'fa-solid fa-angle-right',
				previous: 'fa-solid fa-angle-left'
			}
		});
	}

	if($('.timepicker').length > 0) {
		$('.timepicker').datetimepicker({
			format: "hh:mm:ss",
			icons: {
				up: "fa fa-angle-up",
				down: "fa-solid fa-angle-down",
				next: 'fa-solid fa-angle-right',
				previous: 'fa-solid fa-angle-left'
			}
		});
	}
	
	// Datatable

	if($('.datatable').length > 0) {
		$('.datatable').DataTable({
			"bFilter": false,
			"language": {
                paginate: {
                    next: ' <i class=" fa fa-angle-double-right"></i>',
                    previous: '<i class="fa fa-angle-double-left"></i> '
                },
             },
		});
		
	}
	
	// Tooltip

	if($('[data-bs-toggle="tooltip"]').length > 0) {
		var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
		var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
			return new bootstrap.Tooltip(tooltipTriggerEl)
		})
	}
	
	// Email Inbox

	if($('.clickable-row').length > 0 ){
		$(".clickable-row").click(function() {
			window.location = $(this).data("href");
		});
	}

	// Check all email
	
	$(document).on('click', '#check_all', function() {
		$('.checkmail').click();
		return false;
	});
	if($('.checkmail').length > 0) {
		$('.checkmail').each(function() {
			$(this).on('click', function() {
				if($(this).closest('tr').hasClass('checked')) {
					$(this).closest('tr').removeClass('checked');
				} else {
					$(this).closest('tr').addClass('checked');
				}
			});
		});
	}
	
	// Mail important
	
	$(document).on('click', '.mail-important', function() {
		$(this).find('i.fa').toggleClass('fa-star').toggleClass('fa-star-o');
	});
	
	// CK Editor

	if ($('#editor').length > 0) {
		ClassicEditor
		.create( document.querySelector( '#editor' ), {
			 toolbar: {
			    items: [
			        'heading', '|',
			        'fontfamily', 'fontsize', '|',
			        'alignment', '|',
			        'fontColor', 'fontBackgroundColor', '|',
			        'bold', 'italic', 'strikethrough', 'underline', 'subscript', 'superscript', '|',
			        'link', '|',
			        'outdent', 'indent', '|',
			        'bulletedList', 'numberedList', 'todoList', '|',
			        'code', 'codeBlock', '|',
			        'insertTable', '|',
			        'uploadImage', 'blockQuote', '|',
			        'undo', 'redo'
			    ],
			    shouldNotGroupWhenFull: true
			}
		} )
		.then( editor => {
			window.editor = editor;
		} )
		.catch( err => {
			console.error( err.stack );
		} );
	}
	
	// Task Complete
	
	$(document).on('click', '#task_complete', function() {
		$(this).toggleClass('task-completed');
		return false;
	});
	
	// Multiselect

	if($('#customleave_select').length > 0) {
		$('#customleave_select').multiselect();
	}
	if($('#edit_customleave_select').length > 0) {
		$('#edit_customleave_select').multiselect();
	}

	

	// Leave Settings button show
	
	$(document).on('click', '.leave-edit-btn', function() {
		$(this).removeClass('leave-edit-btn').addClass('btn btn-white leave-cancel-btn').text('Cancel');
		$(this).closest("div.leave-right").append('<button class="btn btn-primary leave-save-btn" type="submit">Save</button>');
		$(this).parent().parent().find("input").prop('disabled', false);
		return false;
	});
	$(document).on('click', '.leave-cancel-btn', function() {
		$(this).removeClass('btn btn-white leave-cancel-btn').addClass('leave-edit-btn').text('Edit');
		$(this).closest("div.leave-right").find(".leave-save-btn").remove();
		$(this).parent().parent().find("input").prop('disabled', true);
		return false;
	});

	$(document).on('click', '#filter_search', function() {
		$('#filter_inputs').slideToggle("slow");
	});
	$(document).on('click', '#filter_search', function() {
		$('#filter_search').toggleClass("active-filter");
	});

	$(document).on('change', '.leave-box .onoffswitch-checkbox', function() {
		var id = $(this).attr('id').split('_')[1];
		if ($(this).prop("checked") == true) {
			$("#leave_"+id+" .leave-edit-btn").prop('disabled', false);
			$("#leave_"+id+" .leave-action .btn").prop('disabled', false);
		}
	    else {
			$("#leave_"+id+" .leave-action .btn").prop('disabled', true);	
			$("#leave_"+id+" .leave-cancel-btn").parent().parent().find("input").prop('disabled', true);
			$("#leave_"+id+" .leave-cancel-btn").closest("div.leave-right").find(".leave-save-btn").remove();
			$("#leave_"+id+" .leave-cancel-btn").removeClass('btn btn-white leave-cancel-btn').addClass('leave-edit-btn').text('Edit');
			$("#leave_"+id+" .leave-edit-btn").prop('disabled', true);
		}
	});
	
	$('.leave-box .onoffswitch-checkbox').each(function() {
		var id = $(this).attr('id').split('_')[1];
		if ($(this).prop("checked") == true) {
			$("#leave_"+id+" .leave-edit-btn").prop('disabled', false);
			$("#leave_"+id+" .leave-action .btn").prop('disabled', false);
		}
	    else {
			$("#leave_"+id+" .leave-action .btn").prop('disabled', true);	
			$("#leave_"+id+" .leave-cancel-btn").parent().parent().find("input").prop('disabled', true);
			$("#leave_"+id+" .leave-cancel-btn").closest("div.leave-right").find(".leave-save-btn").remove();
			$("#leave_"+id+" .leave-cancel-btn").removeClass('btn btn-white leave-cancel-btn').addClass('leave-edit-btn').text('Edit');
			$("#leave_"+id+" .leave-edit-btn").prop('disabled', true);
		}
	});
	
	// Placeholder Hide

	if ($('.otp-input, .zipcode-input input, .noborder-input input').length > 0) {
		$('.otp-input, .zipcode-input input, .noborder-input input').focus(function () {
			$(this).data('placeholder', $(this).attr('placeholder'))
				   .attr('placeholder', '');
		}).blur(function () {
			$(this).attr('placeholder', $(this).data('placeholder'));
		});
	}
	

	
	// OTP Input
	
	if ($('.otp-input').length > 0) {
		$(".otp-input").keyup(function(e) {
			if ((e.which >= 48 && e.which <= 57) || (e.which >= 96 && e.which <= 105)) {
				$(e.target).next('.otp-input').focus();
			} else if (e.which == 8) {
				$(e.target).prev('.otp-input').focus();
			}
		});
	}

	// Contact Wizard
	$(".add-info-fieldset .wizard-next-btn").on('click', function () { // Function Runs On NEXT Button Click
		$(this).closest('fieldset').next().fadeIn('slow');
		$(this).closest('fieldset').css({
			'display': 'none'
		});
		// Adding Class Active To Show Steps Forward;
		$('.progress-bar-wizard .active').removeClass('active').addClass('activated').next().addClass('active');
	});
	
	// Small Sidebar

	$(document).on('click', '#toggle_btn', function() {
		if($('body').hasClass('mini-sidebar')) {
			$('body').removeClass('mini-sidebar');
			$('.subdrop + ul').show();
		} else {
			$('body').addClass('mini-sidebar');
			$('.subdrop + ul').hide();
		}
		return false;
	});
	$(document).on('mouseover', function(e) {
		e.stopPropagation();
		if($('body').hasClass('mini-sidebar') && $('#toggle_btn').is(':visible')) {
			var targ = $(e.target).closest('.sidebar').length;
			if(targ) {
				$('body').addClass('expand-menu');
				$('.subdrop + ul').show();
			} else {
				$('body').removeClass('expand-menu');
				$('.subdrop + ul').hide();
			}
			return false;
		}
	});
	
	$(document).on('click', '.top-nav-search .responsive-search', function() {
		$('.top-nav-search').toggleClass('active');
	});
	
	$(document).on('click', '#file_sidebar_toggle', function() {
		$('.file-wrap').toggleClass('file-sidebar-toggle');
	});
	$(document).on('click', '#file_sidebar_toggle', function() {
		$('.file-wrap').toggleClass('file-sidebar-toggle');
	});
	$(document).on('click', '.list-inline-item .submenu a', function() {
		$('.hidden-links').addClass('hidden');
	});
	$(document).on('click', '.two-col-bar .sub-menu a', function() {
		$('.two-col-bar .sub-menu ul').toggle(500);
	});
	$(document).on('click', '.sidebar-horizantal .viewmoremenu', function() {
		$('.sidebar-horizantal .list-inline-item .submenu ul').hide(500);
		$('.sidebar-horizantal .list-inline-item .submenu a').removeClass("subdrop");
	});
	if($('.kanban-wrap').length > 0) {
		$(".kanban-wrap").sortable({
			connectWith: ".kanban-wrap",
			handle: ".kanban-box",
			placeholder: "drag-placeholder"
		});
	}
	
	
	if ($(window).width() < 991) {
		$("html").each(function() {
			var attributes = $.map(this.attributes, function(item) {
			return item.name;
			});
		
			var img = $(this);
			$.each(attributes, function(i, item) {
			img.removeAttr(item);
			});
		});
	}	
	
	// $(document).on('click', '#customizer-layout02', function() {
	// 	location.reload();
	// });
	
	$(document).ready(function(){
		$("#sidebar-size-compact").click(function(){            
			$('html').attr("data-layout", "vertical");
		});
	});
	$(document).ready(function(){
		$("#sidebar-size-small-hover").click(function(){            
			$('html').attr("data-layout", "vertical");
		});
	});
	$(document).ready(function(){
		$("[data-layout=horizontal] #sidebar-size-compact").click(function(){            
			$('html').attr("data-layout", "vertical");
		});
	});
	$(document).ready(function(){
		$("[data-layout=horizontal] #sidebar-size-small-hover").click(function(){            
			$('html').attr("data-layout", "vertical");
		});
	});
	$(document).ready(function(){
		$(".colorscheme-cardradio input[type=radio]").click(function(){
			$("html").removeAttr("data-topbar");
		});
		
		$(".viewmoremenu").click(function() {
			$(".hidden-links").toggleClass("hidden");
		});
		
	});

	$("[data-sidebar-size=sm-hover] #customizer-layout03").click(function(){
		$("html").removeAttr("data-layout-mode");
	});
	$(".greedy .list-inline-item .submenu a").click(function(){
		$(".hidden-links").addClass("hidden");
	});
	
	// Add Product
	$("#addProduct").on('click',function(e){
		const tableBody = $('#addTable tbody');
        const rowCount = tableBody.find('tr').length + 1;
	var addTable = `<tr><td>${rowCount}</td><td><input class="form-control" type="text" style="min-width:150px"></td>
	  	<td><input class="form-control" type="text" style="min-width:150px"></td>
		<td><input class="form-control" style="width:100px" type="text"></td>
		<td><input class="form-control" style="width:80px" type="text"></td>
		<td><input class="form-control" readonly style="width:120px" type="text"></td>
		<td><a href="javascript:void(0)" class="text-danger font-18 remove" title="Remove"><i class="fa-regular fa-trash-can"></i></a></td></tr>`; 
		$('tbody.tbodyone').append(addTable);
		e.preventDefault();
  	});

	// Add Edit Product 
	
	$("#addEditProduct").on('click',function(e){
		const tableBody = $('#editTable tbody');
        const rowCount = tableBody.find('tr').length + 1;
		var editAddTable = `<tr><td>${rowCount}</td>
	<td>
		<input class="form-control" type="text" value="Vehicle Module" style="min-width:150px">
	</td>
	<td>
		<input class="form-control" type="text" value="Create, edit delete functionlity" style="min-width:150px">
	</td>
	<td>
		<input class="form-control" style="width:100px" type="text" value="112">
	</td>
	<td>
		<input class="form-control" style="width:80px" type="text" value="1">
	</td>
	<td>
		<input class="form-control" readonly style="width:120px" type="text" value="112">
	</td>
	<td>
		<a href="javascript:void(0)" class="text-danger font-18 remove" title="Remove"><i class="fa-regular fa-trash-can"></i></a>
	</td></tr>`;
		$('tbody.tbodyone').append(editAddTable);
		e.preventDefault();
  	});
  
  $(document).on('click','.remove',function(){
	  $(this).parents('tr').remove();
	  const tableBody = $('#addTable tbody');
    const rowCount = tableBody.find('tr').length - 1;
	var editAddTable = `<tr><td>${rowCount}</td></tr>`
});

if($('#collapse-header').length > 0) {
	function toggleFullscreen(elem) {
	  elem = elem || document.documentElement;
	  if (!document.fullscreenElement && !document.mozFullScreenElement &&
		!document.webkitFullscreenElement && !document.msFullscreenElement) {
		if (elem.requestFullscreen) {
		  elem.requestFullscreen();
		} else if (elem.msRequestFullscreen) {
		  elem.msRequestFullscreen();
		} else if (elem.mozRequestFullScreen) {
		  elem.mozRequestFullScreen();
		} else if (elem.webkitRequestFullscreen) {
		  elem.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
		}
	  } else {
		if (document.exitFullscreen) {
		  document.exitFullscreen();
		} else if (document.msExitFullscreen) {
		  document.msExitFullscreen();
		} else if (document.mozCancelFullScreen) {
		  document.mozCancelFullScreen();
		} else if (document.webkitExitFullscreen) {
		  document.webkitExitFullscreen();
		}
	  }
	}

	document.getElementById('collapse-header').addEventListener('click', function() {
	  toggleFullscreen();
	});
}

/* card with fullscreen */
let DIV_CARD = ".card";
let cardFullscreenBtn = document.querySelectorAll(
  '[data-bs-toggle="card-fullscreen"]'
);
cardFullscreenBtn.forEach((ele) => {
  ele.addEventListener("click", function (e) {
	let $this = this;
	let card = $this.closest(DIV_CARD);
	card.classList.toggle("card-fullscreen");
	card.classList.remove("card-collapsed");
	e.preventDefault();
	return false;
  });
});
/* card with fullscreen */

/* card with close button */
let DIV_CARD_CLOSE = ".card";
let cardRemoveBtn = document.querySelectorAll(
	'[data-bs-toggle="card-remove"]'
);
cardRemoveBtn.forEach((ele) => {
	ele.addEventListener("click", function (e) {
	e.preventDefault();
	let $this = this;
	let card = $this.closest(DIV_CARD_CLOSE);
	card.remove();
	return false;
	});
});
/* card with close button */

/* Add Deals Row*/
$(document).on('click','.add-new-deal',function(){

	var dealscontent = '<div class="row">' +
	'<div class="col-md-12">' +
	'<div class="input-block mb-3">' +
	'<div class="d-flex justify-content-between align-items-center">' +
	'<label class="col-form-label">Deals <span class="text-danger">*</span></label>' +
	'</div>' +
	'<select class="select">' +	
	'<option>Select</option>' +	
	'<option>Collins</option>' +	
	'</select>' +	
	'</div>' +
	'</div>' +
	'</div>';
	setTimeout(function () {
		$('.select');
		setTimeout(function () {
			$('.select').select2({
				minimumResultsForSearch: -1,
				width: '100%'
			});
		}, 100);
	}, 100);
	$(".deals-add-col").append(dealscontent);
	return false;
});

/* Add Contact Row*/
$(document).on('click','.add-new-contact',function(){

	var contactcontent =
	'<div class="col-md-12">' +
	'<div class="input-block mb-3">' +
	'<div class="d-flex justify-content-between align-items-center">' +
	'<label class="col-form-label">Contacts <span class="text-danger">*</span></label>' +
	'</div>' +
	'<select class="select">' +	
	'<option>Select</option>' +	
	'<option>Darlee Robertson</option>' +	
	'<option>Sharon Roy</option>' +	
	'</select>' +	
	'</div>' +
	'</div>';
	setTimeout(function () {
		$('.select');
		setTimeout(function () {
			$('.select').select2({
				minimumResultsForSearch: -1,
				width: '100%'
			});
		}, 100);
	}, 100);
	$(".contact-add-col").append(contactcontent);
	return false;
});

/* Add Company Row*/
$(document).on('click','.add-new-company',function(){

	var companycontent =
	'<div class="col-md-12">' +
	'<div class="input-block mb-3">' +
	'<div class="d-flex justify-content-between align-items-center">' +
	'<label class="col-form-label">Company <span class="text-danger">*</span></label>' +
	'</div>' +
	'<select class="select">' +	
	'<option>Select</option>' +	
	'<option>NovaWaveLLC</option>' +	
	'<option>SilverHawk</option>' +	
	'</select>' +	
	'</div>' +
	'</div>';
	setTimeout(function () {
		$('.select');
		setTimeout(function () {
			$('.select').select2({
				minimumResultsForSearch: -1,
				width: '100%'
			});
		}, 100);
	}, 100);
	$(".company-add-col").append(companycontent);
	return false;
});


/* Add Lead Phoneno*/
$(document).on('click', '.delete-lead-phno', function () {
	$(this).closest('.del-phno-col').remove();
	return false;
});
$(document).on('click','.add-lead-phno',function(){

	var phnocontent =
	'<div class="col-md-12 del-phno-col">' +
	'<div class="row">' +
	'<div class="col-lg-8">' +
	'<div class="input-block mb-3">' +
	'<label class="col-form-label">Phone Number <span class="text-danger">*</span></label>' +
	'<input class="form-control" type="text">' +	
	'</div>' +	
	'</div>' +	
	'<div class="col-lg-4 d-flex align-items-end">' +	
	'<div class="input-block mb-3 d-flex align-items-center">' +	
	'<div class="w-100">' +	
	'<select class="select">' +
	'<option>Select</option>' +	
	'<option>Work</option>' +	
	'<option>Home</option>' +	
	'</select>' +	
	'</div>' +
	'<a href="#" class="delete-lead-phno new-delete-icon"><i class="la la-trash"></i></a>' +	
	'</div>'+
	'</div>'+
	'</div>'+
	'</div>';
	setTimeout(function () {
		$('.select');
		setTimeout(function () {
			$('.select').select2({
				minimumResultsForSearch: -1,
				width: '100%'
			});
		}, 100);
	}, 100);
	$(".lead-phno-col").append(phnocontent);
	return false;
});

/* Add Email Phoneno*/
$(document).on('click', '.delete-lead-email', function () {
	$(this).closest('.del-email-col').remove();
	return false;
});
$(document).on('click','.add-lead-email',function(){

	var emailcontent =
	'<div class="col-md-12 del-email-col">' +
	'<div class="row">' +
	'<div class="col-lg-8">' +
	'<div class="input-block mb-3">' +
	'<label class="col-form-label">Email <span class="text-danger">*</span></label>' +
	'<input class="form-control" type="email">' +	
	'</div>' +	
	'</div>' +	
	'<div class="col-lg-4 d-flex align-items-end">' +	
	'<div class="input-block mb-3 d-flex align-items-center">' +	
	'<div class="w-100">' +	
	'<select class="select">' +
	'<option>Select</option>' +	
	'<option>Work</option>' +	
	'<option>Home</option>' +	
	'</select>' +	
	'</div>' +
	'<a href="#" class="delete-lead-email new-delete-icon"><i class="la la-trash"></i></a>' +	
	'</div>'+
	'</div>'+
	'</div>'+
	'</div>';
	setTimeout(function () {
		$('.select');
		setTimeout(function () {
			$('.select').select2({
				minimumResultsForSearch: -1,
				width: '100%'
			});
		}, 100);
	}, 100);
	$(".lead-email-col").append(emailcontent);
	return false;
});

/* Add Pipeline */
$(document).on('click','.add-pipeline-btn',function(){

	var pipelinecontent =
	'<div class="col-md-12">' +
	'<div class="input-block mb-3">' +
	'<div class="d-flex justify-content-between align-items-center">' +
	'<label class="col-form-label">Pipeline <span class="text-danger">*</span></label>' +
	'</div>' +
	'<select class="select">' +
	'<option>Select</option>' +	
	'<option>Sales</option>' +	
	'<option>Marketing</option>' +	
	'<option>Calls</option>' +	
	'</select>' +	
	'</div>' +
	'</div>';
	setTimeout(function () {
		$('.select');
		setTimeout(function () {
			$('.select').select2({
				minimumResultsForSearch: -1,
				width: '100%'
			});
		}, 100);
	}, 100);
	$(".pipeline-add-col").append(pipelinecontent);
	return false;
});

// Add Sign
$(document).on('click', '.trash-sign', function () {
	$(this).closest('.sign-cont').remove();
	return false;
});
$(document).on('click','.add-sign',function(){

	var signcontent = '<div class="row sign-cont">' +
		'<div class="col-md-6">' +
			'<div class="input-block mb-3">' +
				'<input class="form-control" type="text" placeholder="Enter Name">' +
			'</div>' +
		'</div>' +
		'<div class="col-md-6">' +
			'<div class="d-flex align-items-center">' +    
				'<div class="input-block float-none mb-3 me-3">' +    
				'<input class="form-control" type="text" placeholder="Email Address">' +    
				'</div>' +
				'<div class="input-btn mb-3">' +
					'<a href="javascript:void(0);" class="trash-sign"><i class="las la-trash"></i></a>' +
				'</div>' +
			'</div>' +
		'</div>' +
	'</div>';
	$(".sign-content").append(signcontent);
	return false;
});

// Add Comment

if($('.add-comment').length > 0) {
	$(".add-comment").on("click", function(a) {
	  $(this).closest(".notes-editor").children(".note-edit-wrap").slideToggle();
	});
	$(".add-cancel").on("click", function(a) {
	  $(this).closest(".note-edit-wrap").slideUp();
	});
}

if($('.kanban-drag-wrap').length > 0) {
	$(".kanban-drag-wrap").sortable({
		connectWith: ".kanban-drag-wrap",
		handle: ".kanban-card",
		placeholder: "drag-placeholder"
	});
}


// Projects Carousel

if($('.project-slider').length > 0) {
	$('.project-slider').owlCarousel({
		loop:true,
		margin:20,
		nav:true,
		dots:false,
		smartSpeed: 2000,
		autoplay:false,
		navText: [
			'<i class="fa-solid fa-chevron-left"></i>',
			'<i class="fa-solid fa-chevron-right"></i>'
		],
		navContainer: '.project-nav',
		responsive:{
			0:{
				items:1
			},				
			600:{
				items:2
			},
			992:{
				items:2
			},
			1200:{
				items:3
			},
			1400:{
				items:4
			}
		}
	})
}

// Company Carousel

if($('.company-slider').length > 0) {
	$('.company-slider').owlCarousel({
		loop:true,
		margin:20,
		nav:true,
		dots:false,
		smartSpeed: 2000,
		autoplay:false,
		navText: [
			'<i class="fa-solid fa-chevron-left"></i>',
			'<i class="fa-solid fa-chevron-right"></i>'
		],
		navContainer: '.company-nav',
		responsive:{
			0:{
				items:1
			},				
			600:{
				items:2
			},
			992:{
				items:2
			},
			1200:{
				items:2
			},
			1400:{
				items:2
			}
		}
	})
}


if ($(window).width() > 767) {
	if ($('.theiaStickySidebar').length > 0) {
		$('.theiaStickySidebar').theiaStickySidebar({
			// Settings
			additionalMarginTop: 125
		});
	}
}

});
