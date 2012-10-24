(function( window, $, undefined ) {
	
	/*
	* smartresize: событие измения размера отскоком для jQuery
	*
	* Последняя версия доступна на Github:
	* https://github.com/louisremi/jquery.smartresize.js
	*
	* Copyright 2011 @louis_remi
	* Licensed under the MIT license.
	*
	* Перевод: команда сайта RUSELLER.COM
	*/

	var $event = $.event, resizeTimeout;

	$event.special.smartresize 	= {
		setup: function() {
			$(this).bind( "resize", $event.special.smartresize.handler );
		},
		teardown: function() {
			$(this).unbind( "resize", $event.special.smartresize.handler );
		},
		handler: function( event, execAsap ) {
			// Сохраняем контекст
			var context = this,
				args 	= arguments;

			// Устанавливаем корректный тип события
			event.type = "smartresize";

			if ( resizeTimeout ) { clearTimeout( resizeTimeout ); }
			resizeTimeout = setTimeout(function() {
				jQuery.event.handle.apply( context, args );
			}, execAsap === "execAsap"? 0 : 100 );
		}
	};

	$.fn.smartresize 			= function( fn ) {
		return fn ? this.bind( "smartresize", fn ) : this.trigger( "smartresize", ["execAsap"] );
	};
	
	$.Accordion 				= function( options, element ) {
	
		this.$el			= $( element );
		// Пункты списка
		this.$items			= this.$el.children('ul').children('li');
		// Общее количество пунков
		this.itemsCount		= this.$items.length;
		
		// Инициализация аккордеона
		this._init( options );
		
	};
	
	$.Accordion.defaults 		= {
		// Индекс открытого пункта. -1 означает, что все пункты закрыты по умолчанию.
		open			: -1,
		// Если данная опция имеет значение true, то только один пункт может быть открыт. При открытии пункта другой открытый пункт закрывается.
		oneOpenedItem	: false,
		// Скорость анимации открытия/закрытия
		speed			: 600,
		// Эффект анимации открытия/закрытия
		easing			: 'easeInOutExpo',
		// Скорость прокрутки анимации действия
		scrollSpeed		: 900,
		// Эффект прокрутки анимации действия
		scrollEasing	: 'easeInOutExpo'
    };
	
	$.Accordion.prototype 		= {
		_init 				: function( options ) {
			
			this.options 		= $.extend( true, {}, $.Accordion.defaults, options );
			
			// Проверка опций
			this._validate();
			
			// current - индекс открытого пункта
			this.current		= this.options.open;
			
			// Скрываем содержание, чтобы вывести его потом
			this.$items.find('div.st-content').hide();
			
			// Сохраняем оригинальную высоту и координату по вертикали для каждого пункта
			this._saveDimValues();
			
			// Если нужно по умолчанию открыть пункт
			if( this.current != -1 )
				this._toggleItem( this.$items.eq( this.current ) );
			
			// Инициализируем события
			this._initEvents();
			
		},
		_saveDimValues		: function() {
		
			this.$items.each( function() {
				
				var $item		= $(this);
				
				$item.data({
					originalHeight 	: $item.find('a:first').height(),
					offsetTop		: $item.offset().top
				});
				
			});
			
		},
		// Проверка опций
		_validate			: function() {
		
			// Индекс открытого пункта должен иметь значение между -1 и общим количеством пунктов, иначе установим его в значение -1
			if( this.options.open < -1 || this.options.open > this.itemsCount - 1 )
				this.options.open = -1;
	 	
		},
		_initEvents			: function() {
			
			var instance	= this;
			
			// Открыть / закрыть пункт
			this.$items.find('a:first').bind('click.accordion', function( event ) {
				
				var $item			= $(this).parent();
				
				// Закрыть любой открытый пункт если опция oneOpenedItem имеет значнеие true
				if( instance.options.oneOpenedItem && instance._isOpened() && instance.current!== $item.index() ) {
					
					instance._toggleItem( instance.$items.eq( instance.current ) );
				
				}
				
				// Открыть / закрыть пункт
				instance._toggleItem( $item );
				
				return false;
			
			});
			
			$(window).bind('smartresize.accordion', function( event ) {
				
				// Сброс оригинальных занчений пункта
				instance._saveDimValues();
			
				// Сброс высоты содержания любого пункта, который открыт
				instance.$el.find('li.st-open').each( function() {
					
					var $this	= $(this);
					$this.css( 'height', $this.data( 'originalHeight' ) + $this.find('div.st-content').outerHeight( true ) );
				
				});
				
				// Прокрутка до текущего пункта
				if( instance._isOpened() )
				instance._scroll();
				
			});
			
		},
		// Проверка наличия открытых пунктов
		_isOpened			: function() {
		
			return ( this.$el.find('li.st-open').length > 0 );
		
		},
		// Открыть / закрыть пункт
		_toggleItem			: function( $item ) {
			
			var $content = $item.find('div.st-content');
			
			( $item.hasClass( 'st-open' ) ) 
					
				? ( this.current = -1, $content.stop(true, true).fadeOut( this.options.speed ), $item.removeClass( 'st-open' ).stop().animate({
					height	: $item.data( 'originalHeight' )
				}, this.options.speed, this.options.easing ) )
				
				: ( this.current = $item.index(), $content.stop(true, true).fadeIn( this.options.speed ), $item.addClass( 'st-open' ).stop().animate({
					height	: $item.data( 'originalHeight' ) + $content.outerHeight( true )
				}, this.options.speed, this.options.easing ), this._scroll( this ) )
		
		},
		// Прокрутка до текущего или последнего открытого пункта
		_scroll				: function( instance ) {
			/*
			var instance	= instance || this, current;
			
			( instance.current !== -1 ) ? current = instance.current : current = instance.$el.find('li.st-open:last').index();
			
			$('html, body').stop().animate({
				scrollTop	: ( instance.options.oneOpenedItem ) ? instance.$items.eq( current ).data( 'offsetTop' ) : instance.$items.eq( current ).offset().top
			}, instance.options.scrollSpeed, instance.options.scrollEasing );*/
		
		}
	};
	
	var logError 				= function( message ) {
		
		if ( this.console ) {
			
			console.error( message );
			
		}
		
	};
	
	$.fn.accordion 				= function( options ) {
	
		if ( typeof options === 'string' ) {
		
			var args = Array.prototype.slice.call( arguments, 1 );

			this.each(function() {
			
				var instance = $.data( this, 'accordion' );
				
				if ( !instance ) {
					logError( "Нельзя вызвать метод до инициализации; " +
					"попытка вызвать метод '" + options + "'" );
					return;
				}
				
				if ( !$.isFunction( instance[options] ) || options.charAt(0) === "_" ) {
					logError( "Нет такого метода в аккордеоне :'" + options );
					return;
				}
				
				instance[ options ].apply( instance, args );
			
			});
		
		} 
		else {
		
			this.each(function() {
				var instance = $.data( this, 'accordion' );
				if ( !instance ) {
					$.data( this, 'accordion', new $.Accordion( options, this ) );
				}
			});
		
		}
		
		return this;
		
	};
	
})( window, jQuery );